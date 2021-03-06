﻿using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class UserMessage : IEntity
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public Guid UserToId { get; set; }
        public User To { get; set; }
        public Guid UserFromId { get; set; }
        public User From { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}