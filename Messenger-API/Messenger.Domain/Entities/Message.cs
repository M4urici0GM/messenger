using System;
using Messenger.Domain.Interfaces;

namespace Messenger.Domain.Entities
{
    public class Message : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public bool IsReceived { get; set; }
        public bool IsSeen { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime? DateSeen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}