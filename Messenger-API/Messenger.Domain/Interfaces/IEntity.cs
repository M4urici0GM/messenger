using System;

namespace Messenger.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool? IsActive { get; set; }
    }
}