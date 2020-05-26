using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;
using Application.Contexts.WebSockets.Notifications;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares
{
    public class WebSocketsMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IMediator _mediator;
        private readonly ILogger<WebSocketsMiddleware> _logger;
        
        public WebSocketsMiddleware(RequestDelegate requestDelegate, IMediator mediator, ILogger<WebSocketsMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.WebSockets.IsWebSocketRequest && httpContext.Request.Path == "/ws")
            {
                await HandleWebSocketConnection(httpContext);                
            }
            else
            {
                await _requestDelegate(httpContext);
            }
        }

        private async Task HandleWebSocketConnection(HttpContext httpContext)
        {
            var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
            await _mediator.Publish(new OnWebSocketConnected { WebSocket = webSocket });
            byte[] buffer = new byte[4096];
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.Count > 0)
                {
                    await _mediator.Publish(new OnWebSocketMessageReceived
                    {
                        Buffer = buffer,
                        ReceivedMessage = result
                    });
                }
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }
    }
}