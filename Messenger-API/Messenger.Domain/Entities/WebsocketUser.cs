using System;
using System.Data.SqlTypes;
using System.Net.WebSockets;

namespace Messenger.Domain.Entities
{
    public class WebsocketUser
    {
        public string ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public string UserIdentifier { get; set; }
    }
}