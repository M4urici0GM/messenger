
using System;
using Messenger.Domain.Enums;

namespace Messenger.Domain.Entities
{
    public class WebsocketMessage
    {
        public WebsocketMessageType MessageType { get; set; }
        public Guid RequestId { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}