using System.Linq;
using System.Net.Http;
using API;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Contexts;

namespace IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient testClient;

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<MainDbContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<IMainDbContext, MainDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(nameof(MainDbContext));
                        });
                    });
                });

            testClient = appFactory.CreateClient();
        }
    }
}