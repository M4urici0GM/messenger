using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace Application.Contexts.WebSockets.Notifications
{
    public class OnWebSocketConnected : INotification
    {
        public WebSocket WebSocket { get; set; }
        
        public class OnWebSocketConnectedHandler : INotificationHandler<OnWebSocketConnected>
        {
            private readonly ILogger<OnWebSocketConnectedHandler> _logger;

            public OnWebSocketConnectedHandler(ILogger<OnWebSocketConnectedHandler> logger)
            {
                _logger = logger;
            }
            
            public Task Handle(OnWebSocketConnected request, CancellationToken cancellationToken)
            {
                
                _logger.LogInformation("WebSocket connected");
                
                return Task.FromResult(Unit.Value);
            }
        }
    }
}