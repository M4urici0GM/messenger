using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;

namespace Messenger.Application.Services
{
    public class WebsocketManager : IWebsocketManager
    {
        private readonly List<WebsocketUser> _webSockets;

        public WebsocketManager()
        {
            _webSockets = new List<WebsocketUser>();
        }

        public WebsocketUser GetWebsocket(WebSocket webSocket)
        {
            return _webSockets.FirstOrDefault(x => x.WebSocket == webSocket);
        }
        
        public WebsocketUser GetWebsocket(Guid id)
        {
            return _webSockets.FirstOrDefault(x => x.Id == id);
        }

        public WebsocketUser GetWebsocket(User user)
        {
            if (user == null)
                return null;

            return _webSockets.FirstOrDefault(x => x.User.Id == user.Id);
        }

        public Guid AddWebsocket(WebsocketUser websocketUser)
        {
            _webSockets.Add(websocketUser);
            return websocketUser.Id;
        }

        public async Task SendMessage(byte[] message, Guid webSocketId, CancellationToken cancellationToken)
        {
            WebsocketUser webSocket = GetWebsocket(webSocketId);
            if (webSocket == null)
                return;

            await webSocket.WebSocket.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Binary, true,
                cancellationToken);
        }

        public async Task SendMessage(byte[] message, Guid webSocketId)
        {
            await SendMessage(message, webSocketId, CancellationToken.None);
        }

        public async Task SendMessage(byte[] message, User user)
        {
            WebsocketUser webSocket = GetWebsocket(user);
            if (webSocket == null)
                return;

            await SendMessage(message, webSocket.Id, CancellationToken.None);
        }

        public async Task CloseWebsocket(Guid webSocketId, WebSocketCloseStatus webSocketCloseStatus,
            string statusDescription, CancellationToken cancellationToken)
        {
            WebsocketUser webSocket = GetWebsocket(webSocketId);
            if (webSocket == null)
                return;

            await webSocket.WebSocket.CloseAsync(webSocketCloseStatus, statusDescription, cancellationToken);
        }

        public async Task CloseWebsocket(User user, WebSocketCloseStatus webSocketCloseStatus,
            string statusDescription, CancellationToken cancellationToken)
        {
            WebsocketUser webSocket = GetWebsocket(user);
            if (webSocket == null)
                return;
            await CloseWebsocket(webSocket.Id, webSocketCloseStatus, statusDescription, cancellationToken);
        }

        public async Task CloseWebsocket(Guid webSocketId, CancellationToken cancellationToken)
        {
            await CloseWebsocket(webSocketId, WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }

        public async Task CloseWebsocket(User user, CancellationToken cancellationToken)
        {
            await CloseWebsocket(user, WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }
    }
}