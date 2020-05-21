using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public  class EntityAlreadyExists : Exception
    {

        public EntityAlreadyExists(string name, object key)
            :base($"Entity {name} ({key}) already exists")
        { }
    }
}
