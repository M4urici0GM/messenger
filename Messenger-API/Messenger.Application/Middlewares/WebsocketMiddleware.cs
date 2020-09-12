using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Application.EntitiesContext.Websocket.Commands;
using Messenger.Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

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
            WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
            TaskCompletionSource<object> socketFinishedTcs = new TaskCompletionSource<object>();

            Guid? userId = httpContext.User.Identity.GetUserId();
            //TODO: Refactor this code.
            if (userId != null)
            {
                try
                {
                    await _mediator.Send(new AcceptWebsocketConnection
                    {
                        WebSocket = webSocket,
                        TaskCompletionSource = socketFinishedTcs,
                        UserId = userId.Value,
                    });
                }
                catch (Exception ex)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, ex.Message,
                        CancellationToken.None);
                    socketFinishedTcs.SetResult(false);
                    return;
                }
            }
            else
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Missing user authentication",
                    CancellationToken.None);
                socketFinishedTcs.SetResult(false);
                return;
            }

            await socketFinishedTcs.Task;
        }
    }
}