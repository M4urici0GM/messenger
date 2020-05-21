using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            ConfigureSecurity(services, configuration);
            return services;
        }

        private static void ConfigureSecurity(IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}