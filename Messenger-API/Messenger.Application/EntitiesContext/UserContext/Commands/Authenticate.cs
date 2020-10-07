using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.EntitiesContext.AuthenticationContext.Commands;
using Messenger.Application.EntitiesContext.UserContext.Validators;
using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Messenger.Application.EntitiesContext.UserContext.Commands
{
    public class Authenticate : IRequest<AuthenticatedUserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AuthenticateHandler : IRequestHandler<Authenticate, AuthenticatedUserDto>
        {
            private readonly ISecurityService _securityService;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IUserRepository _userRepository;

            public AuthenticateHandler(IUserRepository userRepository, ISecurityService securityService, IMapper mapper, IMediator mediator)
            {
                _userRepository = userRepository;
                _securityService = securityService;
                _mapper = mapper;
                _mediator = mediator;
            }
            
            public async Task<AuthenticatedUserDto> Handle(Authenticate request, CancellationToken cancellationToken)
            {
                AuthenticateValidator validator = new AuthenticateValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(User), request, validationResult.Errors);

                User user = await _userRepository.Get(Builders<User>.Filter.Eq(x => x.Username, request.Username),
                    cancellationToken);

                if (user == null)
                    throw new InvalidCredentialException("Invalid username or password");

                bool hasCorrectPassword = await _securityService.VerifyPassword(request.Password, user.Password);
                
                if (!hasCorrectPassword)
                    throw new InvalidCredentialException("Invalid username or password");

                try
                {
                    WebToken webToken = await _mediator.Send(new GenerateWebToken() { User = user }, cancellationToken);

                    return new AuthenticatedUserDto
                    {
                        User = _mapper.Map<UserDto>(user),
                        Token = webToken,
                    };
                }
                catch (EntityNotFoundException ex)
                {
                    throw new InvalidCredentialException("", ex);
                }
            }
        }
    }
}