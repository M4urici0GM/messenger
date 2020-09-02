using System;
using Messenger.Domain.Entities;

namespace Messenger.Application.Interfaces
{
    public interface IWebsocketManager
    {
        WebsocketUser GetWebsocket(Guid id);
        WebsocketUser GetWebsocket(User user);
        void AddWebsocket(WebsocketUser websocketUser);
    }
}