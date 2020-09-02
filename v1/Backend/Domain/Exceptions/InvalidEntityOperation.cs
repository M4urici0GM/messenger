using System;

namespace Domain.Exceptions
{
    public class InvalidEntityOperation : Exception
    {
        public InvalidEntityOperation(string name, object key) : base($"The entity {name} ({key}) is invalid here")
        {
            
        }
    }
}