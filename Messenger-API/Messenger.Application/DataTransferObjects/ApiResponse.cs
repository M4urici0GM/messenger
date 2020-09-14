using System;
using System.Net;

namespace Messenger.Application.DataTransferObjects
{
    public class ApiResponse
    {
        public HttpStatusCode Status { get; set; }
        public Guid RequestId { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}