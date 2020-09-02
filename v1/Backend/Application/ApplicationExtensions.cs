using System;
using System.Reflection;
using Application.AutoMapper;
using Application.Interfaces.Security;
using Application.Middlewares;
using Application.Security.WebToken;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
            ConfigureSecurity(services, configuration);
            ConfigureAutoMapper(services);
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        
        private static void ConfigureSecurity(IServiceCollection services, IConfiguration configuration)
        {
            bool configurationExists = configuration.GetSection(nameof(TokenConfiguration)).Exists();
            if (!configurationExists)
                throw new InvalidOperationException("Missing token configuration");
            
            TokenConfiguration tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<ITokenConfiguration>(
                configuration.GetSection(nameof(TokenConfiguration)))
                .Configure(tokenConfiguration);
            
            services.AddSingleton<ITokenConfiguration>(tokenConfiguration);
            services.AddSingleton<ISignInConfiguration, SignInConfiguration>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = tokenConfiguration.Audience,
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidateAudience =  true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });
        }

        public static void UseApplication(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseMiddleware(typeof(ErrorMiddleware));
            app.UseMiddleware(typeof(WebSocketsMiddleware));
        }
    }
}