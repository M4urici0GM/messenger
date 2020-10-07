using System;
using System.Collections.Generic;
using Messenger.Domain.Entities;

namespace Messenger.Application.Interfaces.Services
{
    public interface IWebSocketManager
    {
        WebsocketUser GetClient(string connectionId);
        WebsocketUser GetClient(Guid userId);
        void AddClient(WebsocketUser websocketUser);
        void RemoveClient(string connectionId);
        IEnumerable<WebsocketUser> GetClients(Guid userId);
        IEnumerable<WebsocketUser> GetClients(string connectionId);
    }
}