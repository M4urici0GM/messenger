using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Messenger.Application.Hubs;
using Messenger.Application.Interfaces;
using Messenger.Application.Interfaces.JWT;
using Messenger.Application.Interfaces.Services;
using Messenger.Application.Middlewares;
using Messenger.Application.Options.AutoMapper;
using Messenger.Application.Options.JWT;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Messenger.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureJwtOptions(services, configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<ISecurityService, SecurityService>();
            // services.AddSignalR(options =>
            // {
            //     options.HandshakeTimeout = TimeSpan.FromMinutes(2);
            //     options.EnableDetailedErrors = true;
            // });
            ConfigureAutoMapper(services);
        }

        private static void ConfigureJwtOptions(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection jwtConfigurationSection = configuration.GetSection(nameof(JwtOptions));
            JwtOptions jwtOptions = new JwtOptions();

            if (!jwtConfigurationSection.Exists())
                throw new InvalidOperationException($"Missing {nameof(JwtOptions)} Configuration section");

            ConfigureFromConfigurationOptions<JwtOptions> jwtConfigure =
                new ConfigureFromConfigurationOptions<JwtOptions>(jwtConfigurationSection);

            jwtConfigure.Configure(jwtOptions);
            IJwtSigningOptions jwtSigningOptions = new JwtSigningOptions(jwtOptions);

            services.AddSingleton<IJwtOptions>(jwtOptions);
            services.AddSingleton(jwtSigningOptions);
            services.AddSingleton<IWebSocketManager, WebSocketManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = jwtSigningOptions.SigningCredentials.Key
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/ws"))
                            context.Token = accessToken;
                
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(auth =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy);
            });
            
            services.AddControllers(options =>
                {
                    AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            services
                .AddSignalR(options => options.EnableDetailedErrors = true)
                .AddJsonProtocol();
            services.AddHttpContextAccessor();
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        
    }
}