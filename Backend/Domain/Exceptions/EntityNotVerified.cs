using System;

namespace Domain.Exceptions
{
    public class EntityNotVerified : Exception
    {
        public EntityNotVerified(string name, object key)
            : base($"Entity {name} ({key}) is not verified yet.")
        {
            
        }
    }
}
