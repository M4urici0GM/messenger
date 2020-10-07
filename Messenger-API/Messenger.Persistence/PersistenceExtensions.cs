using System;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Context;
using Messenger.Persistence.Options;
using Messenger.Persistence.Repositories.Entities.Security;
using Messenger.Persistence.Repositories.Entities.Users;
using Messenger.Persistence.Repositories.Interfaces.Security;
using Messenger.Persistence.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

        public static void UseMongo(this IServiceCollection services, IConfiguration configuration)
        {
            IMongoConnectionOptions mongoConnectionOptions = new MongoConnectionOptions();
            
            new ConfigureFromConfigurationOptions<IMongoConnectionOptions>(
                configuration.GetSection(nameof(MongoConnectionOptions)))
                .Configure(mongoConnectionOptions);

            services.AddSingleton(mongoConnectionOptions);
            services.AddSingleton<IMongoDbContext, MongoDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}