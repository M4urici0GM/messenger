using System;
using System.Linq;
using System.Net.Mime;
using Application.HealthChecks.CustomChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Application.HealthChecks
{
    public static class HealthChecksExtensions
    {

        public static void ConfigureApplicationHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<WebSocketHealthCheck>("WebSocket");
        }

        public static void StartHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/healthcheck", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(new
                    {
                        ApplicationStatus = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(x => new
                        {
                            CheckName = x.Key,
                            HasError = x.Value.Exception != null,
                            ErrorMessage = x.Value.Exception?.Message,
                            Status = Enum.GetName(typeof(HealthStatus), x.Value.Status),
                        })
                    });

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}