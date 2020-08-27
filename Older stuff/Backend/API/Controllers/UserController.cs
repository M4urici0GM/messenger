using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Contexts.Users.Commands;
using Application.Contexts.Users.Queries;
using Application.DataTransferObjects;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
            :base (mediator, httpContextAccessor)
        { }
        
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInfo request)
        {
            UserDto user = await Mediator.Send(request);
            return Ok(user);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser request)
        {
            UserDto user = await Mediator.Send(request);
            return Ok(user);
        }

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUser request)
        {
            AuthenticatedUserDto authenticatedUser = await Mediator.Send(request);
            return Ok(authenticatedUser);
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            UserDto currentUser = await Mediator.Send(new GetCurrentUser());
            return Ok(currentUser);
        }
    }
}