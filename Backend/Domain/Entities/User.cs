using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        /**
         * Navigation Properties
         */
        public IEnumerable<UserMessage> SentMessages { get; set; }
        public IEnumerable<UserMessage> ReceivedMessages { get; set; }
    }
}