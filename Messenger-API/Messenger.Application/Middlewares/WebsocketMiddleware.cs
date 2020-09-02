using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Application.EntitiesContext.Websocket.Commands;
using Microsoft.AspNetCore.Http;

namespace Messenger.Application.Middlewares
{
    public class WebsocketMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IMediator _mediator;

        public WebsocketMiddleware(RequestDelegate requestDelegate, IMediator mediator)
        {
            _requestDelegate = requestDelegate;
            _mediator = mediator;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.WebSockets.IsWebSocketRequest && httpContext.Request.Path == "/ws")
                await HandleWebsocketConnection(httpContext);
            else
                await _requestDelegate(httpContext);
        }

        private async Task HandleWebsocketConnection(HttpContext httpContext)
        {
            var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
            var socketFinishedTcs = new TaskCompletionSource<object>();

            await _mediator.Send(new WebsocketConnected
            {
                WebSocket = webSocket,
                TaskCompletionSource = socketFinishedTcs
            });

            await socketFinishedTcs.Task;
        }
    }
}