using System;

namespace Messenger.Domain.Entities
{
    public class Message
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public DateTime DateSent { get; set; }
    }
}