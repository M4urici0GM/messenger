using System;
using System.Data.SqlTypes;
using System.Net.WebSockets;

namespace Messenger.Domain.Entities
{
    public class WebsocketUser
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public WebSocket WebSocket { get; set; }

        public WebsocketUser()
        {
            Id = Guid.NewGuid();
        }
    }
}