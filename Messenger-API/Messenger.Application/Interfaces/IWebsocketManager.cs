using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Messenger.Domain.Entities;

namespace Messenger.Application.Interfaces
{
    public interface IWebsocketManager
    {
        WebsocketUser GetWebsocket(WebSocket webSocket);
        WebsocketUser GetWebsocket(Guid id);
        WebsocketUser GetWebsocket(User user);
        Guid AddWebsocket(WebsocketUser websocketUser);
        Task SendMessage(byte[] message, Guid webSocketId, CancellationToken cancellationToken);
        Task SendMessage(byte[] message, Guid webSocketId);
        Task SendMessage(byte[] message, User user);
        Task CloseWebsocket(Guid webSocketId, WebSocketCloseStatus webSocketCloseStatus,
            string statusDescription, CancellationToken cancellationToken);
        Task CloseWebsocket(User user, WebSocketCloseStatus webSocketCloseStatus,
            string statusDescription, CancellationToken cancellationToken);
        Task CloseWebsocket(Guid webSocketId, CancellationToken cancellationToken);
        Task CloseWebsocket(User user, CancellationToken cancellationToken);
    }
}