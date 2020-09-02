using System;
using System.Runtime;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(nameof(MainDbContext));
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Missing connection string");

            services.AddDbContext<IMainDbContext, MainDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}