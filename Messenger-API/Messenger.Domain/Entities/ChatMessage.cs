using System;
using System.Collections.Generic;
using Messenger.Domain.Interfaces;

namespace Messenger.Domain.Entities
{
    public class ChatMessage : IEntity
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public List<User> WhoSaw { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}