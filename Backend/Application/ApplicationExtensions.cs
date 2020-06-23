using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using Application.AutoMapper;
using Application.Configurations;
using Application.Configurations.WebToken;
using Application.HealthChecks;
using Application.Interfaces.Configurations;
using Application.Interfaces.Security;
using Application.Middlewares;
using Application.Services.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddWebSockets(options =>
            {
                options.ReceiveBufferSize = 4096;
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });
            
            services.ConfigureSecurity(configuration);
            services.ConfigureAutoMapper();
            services.ConfigureApplicationHealthCheck();

            bool appConfigurationExists = configuration.GetSection(nameof(AppConfiguration)).Exists();
            if (!appConfigurationExists)
                throw new InvalidOperationException("Missing App Configuration");
            
            AppConfiguration appConfiguration = new AppConfiguration();
            
            new ConfigureFromConfigurationOptions<IAppConfiguration>(
                configuration.GetSection(nameof(AppConfiguration)))
                .Configure(appConfiguration);

            services.AddSingleton<IAppConfiguration>(appConfiguration);
            services.AddTransient<IEncryptService, EncryptService>();
            services.AddHttpContextAccessor();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        
        private static void ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            bool configurationExists = configuration.GetSection(nameof(TokenConfiguration)).Exists();
            if (!configurationExists)
                throw new InvalidOperationException("Missing token configuration");

            TokenConfiguration tokenConfiguration = new TokenConfiguration();
            
            new ConfigureFromConfigurationOptions<ITokenConfiguration>(
                configuration.GetSection(nameof(TokenConfiguration)))
                .Configure(tokenConfiguration);
            
            SignInConfiguration signInConfiguration = new SignInConfiguration(tokenConfiguration);
            
            services.AddSingleton(tokenConfiguration);
            services.AddSingleton<ISignInConfiguration>(signInConfiguration);


            services.AddSingleton<ITokenConfiguration>(tokenConfiguration);
            services.AddSingleton<ISignInConfiguration, SignInConfiguration>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                
                var validationParameters = options.TokenValidationParameters;

                validationParameters.ValidateAudience = true;
                validationParameters.ValidateIssuer = true;
                validationParameters.ClockSkew = TimeSpan.Zero;

                validationParameters.ValidAudience = tokenConfiguration.Audience;
                validationParameters.ValidIssuer = tokenConfiguration.Issuer;
                validationParameters.RequireExpirationTime = true;

                validationParameters.IssuerSigningKey = signInConfiguration.SecurityKey;
            });
        }

        public static void UseApplication(this IApplicationBuilder app)
        {
            app.StartHealthCheck();
            app.UseWebSockets();
            app.UseMiddleware(typeof(ErrorMiddleware));
            app.UseMiddleware(typeof(WebSocketsMiddleware));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}