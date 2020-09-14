using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Messenger.API.Utils;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.EntitiesContext.UserContext.Commands;
using Messenger.Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : BaseController
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid userId)
        {
            if (userId.Equals(Guid.Empty))
                return CreateResponse(HttpStatusCode.BadRequest, "Missing User's Id");
            UserDto userDto = await _mediator.Send(new GetUser {Id = userId});
            return CreateResponse(HttpStatusCode.OK, userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUser request)
        {
            UserDto userDto = await _mediator.Send(request);
            return CreateResponse(HttpStatusCode.Created, userDto);
        }
    }
}