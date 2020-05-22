using System;
using System.Linq;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace IntegrationTests
{
    public class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptors = services.Where(
                    d => d.ServiceType == typeof(DbContextOptions<MainDbContext>)
                    || d.ServiceType == typeof(IMainDbContext)).ToList();

                if (descriptors.Count > 0)
                {
                    descriptors.ForEach(descriptor =>
                    {
                        services.Remove(descriptor);
                    });
                }
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<IMainDbContext, MainDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();
                
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb =  (DbContext) scopedServices.GetRequiredService<IMainDbContext>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                }
            });
        }
    }
}