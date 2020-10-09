using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Messenger.Application.Interfaces.Services;
using Messenger.Domain.Entities;

namespace Messenger.Application.Services
{
    public class WebSocketManager : IWebSocketManager
    {
        private readonly List<WebsocketUser> _connectedClients;

        public WebSocketManager()
        {
            _connectedClients  = new List<WebsocketUser>();
        }

        public WebsocketUser GetClient(string connectionId) =>
            _connectedClients.FirstOrDefault(x => x.ConnectionId == connectionId);

        public WebsocketUser GetClient(Guid userId) =>
            _connectedClients.FirstOrDefault(x => x.UserId == userId);

        public void AddClient(WebsocketUser websocketUser) => _connectedClients.Add(websocketUser);

        public void RemoveClient(string connectionId) => _connectedClients
            .RemoveAll(x => x.ConnectionId == connectionId);

        public IEnumerable<WebsocketUser> GetClients(Guid userId) => _connectedClients
            .FindAll(x => x.UserId == userId);

        public IEnumerable<WebsocketUser> GetClients(string connectionId) =>
            _connectedClients.FindAll(x => x.ConnectionId == connectionId);
    }
}