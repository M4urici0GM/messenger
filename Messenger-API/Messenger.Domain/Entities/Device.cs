using System;
using Messenger.Domain.Interfaces;

namespace Messenger.Domain.Entities
{
    public class Device : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}