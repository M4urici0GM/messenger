using Messenger.Application.Interfaces;
using Messenger.Application.Middlewares;
using Messenger.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IWebsocketManager, WebsocketManager>();
        }

        public static void UseApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(WebsocketMiddleware));
        }
        
    }
}