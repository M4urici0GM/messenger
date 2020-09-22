using System;
using Messenger.Domain.Interfaces;

namespace Messenger.Domain.Entities
{
    public class ChatUser : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}