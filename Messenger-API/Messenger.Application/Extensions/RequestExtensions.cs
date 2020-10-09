using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MediatR;

namespace Messenger.Application.Extensions
{
    public static class RequestExtensions
    {
        public static void MapParams(this IBaseRequest request, Dictionary<string, object> parameters)
        {
            if (parameters.Count == 0)
                return;
            
            PropertyInfo[] myProperties = request.GetType().GetProperties();
            
            foreach (KeyValuePair<string, object> keyValuePair in parameters)
            {
                PropertyInfo myProperty = myProperties.FirstOrDefault(x => x.Name == keyValuePair.Key);
                if (myProperty == null)
                    continue;
                
                myProperty.SetValue(request, keyValuePair.Value);
            }
        }
    }
}