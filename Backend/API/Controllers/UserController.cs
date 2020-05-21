using System;
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
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(new
            {
                message = "Hello World"
            });
        }

        [HttpPut]
        public async Task<IActionResult> CreateUser([FromBody]CreateUser request)
        {
            try
            {
                UserDto newUser = await _mediator.Send(request);
                return Ok(new
                {
                    user = newUser
                });
            }
            catch (EntityValidationException e)
            {
                return BadRequest(new
                {
                    errors = e.Failures,
                    message = "One or more fields are invalid."
                });
            }
            catch (EntityAlreadyExists e)
            {
                return Conflict(new
                {
                    message = e.Message
                });
            }
        }
    }
}