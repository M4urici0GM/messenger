using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.HealthChecks.CustomChecks
{
    public class WebSocketHealthCheck : IHealthCheck
    {

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var socket = new ClientWebSocket();
                await socket.ConnectAsync(new Uri("wss://localhost:5001/ws"), cancellationToken);
                if (socket.State == WebSocketState.Open)
                    return HealthCheckResult.Healthy("WebSocket server is listening and healthy");
                return HealthCheckResult.Degraded("Problem trying to connect to the websocket server");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Exception trying to connect to the server", ex);
            }
        }
    }
}