using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Message : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string MimeType { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? SeenDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}