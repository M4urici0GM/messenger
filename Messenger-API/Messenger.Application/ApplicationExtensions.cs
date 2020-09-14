using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Messenger.Application.Interfaces;
using Messenger.Application.Middlewares;
using Messenger.Application.Options.AutoMapper;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IWebsocketManager, WebsocketManager>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddWebSockets(options =>
            {
                options.AllowedOrigins.Add("*");
                options.ReceiveBufferSize = 4096;
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });
            ConfigureAutoMapper(services);
        }

        public static void UseApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorMiddleware));
            app.UseMiddleware(typeof(WebsocketMiddleware));
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