using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Messenger.API.Utils;
using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.DataTransferObjects.Validators.Users;
using Messenger.Application.EntitiesContext.UserContext.Commands;
using Messenger.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUser request)
        {
            UserDto userDto = await _mediator.Send(request);
            return CreateResponse(HttpStatusCode.Created, userDto);
        }

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDto request)
        {
            AuthenticateDtoValidator validator = new AuthenticateDtoValidator();
            ValidationResult validationResult = await validator.ValidateAsync(request);
            
            if (!validationResult.IsValid)
                throw new EntityValidationException("Authentication", request, validationResult.Errors);

            AuthenticatedUserDto userDto  = (request.GrantType == "password")
                ? await _mediator.Send(new Authenticate
                    {
                        Username = request.Username,
                        Password = request.Password
                    })
                : await _mediator.Send(new AuthWithRefreshToken {  Token = request.Token });
            
            return CreateResponse(HttpStatusCode.OK, userDto);
        }
    }
}