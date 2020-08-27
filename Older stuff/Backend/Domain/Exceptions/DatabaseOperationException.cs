using System;

namespace Domain.Exceptions
{
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string entityName, object key)
            :base($"There's a exception while trying to execute operation on entity {entityName} ({key})")
        {
            
        }
    }
}