using System.Net;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Utils
{
    public class BaseController : ControllerBase
    {
        public IActionResult CreateResponse(HttpStatusCode status, string message, object response)
        {
            ApiResponse apiResponse = new ApiResponse
            {
                Message = message,
                Status = status,
                RequestId = HttpContext.GetRequestId(),
                Response = response,
            };
            
            if (status == HttpStatusCode.BadRequest)
                return BadRequest(apiResponse);

            if (status == HttpStatusCode.Created)
                return Created("", apiResponse);

            return Ok(apiResponse);
        }

        public IActionResult CreateResponse(HttpStatusCode status, object response)
        {
            return CreateResponse(status, null, response);
        }

        public IActionResult CreateResponse(HttpStatusCode status, string message)
        {
            return CreateResponse(status, message, null);
        }
    }
}