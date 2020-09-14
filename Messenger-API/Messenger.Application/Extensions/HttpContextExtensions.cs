using System;
using Microsoft.AspNetCore.Http;

namespace Messenger.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetRequestId(this HttpContext context)
        {
            bool hasRequestId = context.Items.TryGetValue("RequestId", out object guid);
            if (!hasRequestId)
                return Guid.Empty;

            return (Guid) guid;
        }
    }
}