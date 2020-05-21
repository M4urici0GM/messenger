using System;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IMainDbContext, MainDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(MainDbContext)));
            });
            
            return services;
        }
    }
}