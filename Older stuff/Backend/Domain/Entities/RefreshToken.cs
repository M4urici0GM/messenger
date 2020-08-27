using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        /**
         * Navigation Properties
         */
        public User User { get; set; }
    }
}