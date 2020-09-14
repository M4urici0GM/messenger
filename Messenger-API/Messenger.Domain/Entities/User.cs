﻿using System;
using Messenger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
    }
}