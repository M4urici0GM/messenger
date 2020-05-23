using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Contexts.Users.Commands;
using Application.DataTransferObjects;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInfo request)
        {
            UserDto user = await _mediator.Send(request);
            return Ok(user);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser request)
        {
            UserDto user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUser request)
        {
            AuthenticatedUserDto authenticatedUser = await _mediator.Send(request);
            return Ok();
        }
    }
}