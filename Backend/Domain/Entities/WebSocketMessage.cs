
using Domain.Enum;

namespace Domain.Entities
{
    public class WebSocketMessage
    {
        public WebSocketMessageType MessageType { get; set; }
        public byte[] Content { get; set; }
    }
}