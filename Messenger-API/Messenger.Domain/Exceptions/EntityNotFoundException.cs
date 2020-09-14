using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string nameof, object key)
            : base($"Entity of type {nameof} ({key}) was not found.")
        {

        }
    }
}
