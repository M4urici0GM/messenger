using System;
using System.Collections.Generic;
using Messenger.Domain.Enums;
using Messenger.Domain.Interfaces;

namespace Messenger.Domain.Entities
{
    public class Chat : IEntity
    {
        public Guid Id { get; set; }
        public ChatType ChatType { get; set; }
        public string Name { get; set; }
        public List<ChatUser> Users { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}