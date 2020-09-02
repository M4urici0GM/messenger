using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata;
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

        public WebsocketUser GetWebsocket(Guid id)
        {
            return _webSockets.FirstOrDefault(x => x.Id == id);
        }


        public WebsocketUser GetWebsocket(User user)
        {
            return _webSockets.FirstOrDefault(x => x.User.Id == user.Id);
        }

        public void AddWebsocket(WebsocketUser websocketUser)
        {
            _webSockets.Add(websocketUser);
        }
    }
}