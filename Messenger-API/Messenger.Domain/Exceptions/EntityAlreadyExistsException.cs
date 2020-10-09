using System;

namespace Messenger.Domain.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string name, object key)
            : base($"Entity type of {name} with key {key} already exists")
        {
            
        }
    }
}