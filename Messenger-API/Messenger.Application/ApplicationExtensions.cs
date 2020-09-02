using Messenger.Application.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            
        }

        public static void UseApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(WebsocketMiddleware));
        }
        
    }
}