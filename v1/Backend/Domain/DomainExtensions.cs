using System;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}