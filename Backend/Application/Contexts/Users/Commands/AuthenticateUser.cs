using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Contexts.Authentication.Commands;
using Application.Contexts.Users.Validators;
using Application.DataTransferObjects;
using Application.Interfaces;
using Application.Interfaces.Configurations;
using Application.Interfaces.Security;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts.Users.Commands
{
    public class AuthenticateUser : IRequest<AuthenticatedUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }

        public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, AuthenticatedUserDto>
        {
            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IEncryptService _encryptService;

            public AuthenticateUserHandler(IMainDbContext mainDbContext, IMapper mapper,
                IMediator mediator, IEncryptService encryptService)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
                _encryptService = encryptService;
                _mediator = mediator;
            }

            public async Task<AuthenticatedUserDto> Handle(AuthenticateUser request,
                CancellationToken cancellationToken)
            {
                AuthenticateUserValidator validator = new AuthenticateUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(AuthenticateUser), request, validationResult.Errors);

                User user = await _mainDbContext.Users
                    .AsNoTracking()
                    .Where(u => u.Email == request.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user.Equals(null))
                    throw new InvalidCredentialException();

                string passwordHash = user.Password;
                bool isPasswordValid = await _encryptService.VerifyHash(request.Password, passwordHash);

                if (!isPasswordValid)
                    throw new InvalidCredentialException();
                
                var generateTokenRequest = new GenerateToken
                {
                    Claims = new List<Claim>()
                    {
                        new Claim("UserId", user.Id.ToString("D"))
                    }
                };

                WebToken webToken = await _mediator.Send(generateTokenRequest, cancellationToken);

                return new AuthenticatedUserDto
                {
                    User = _mapper.Map<UserDto>(user),
                    WebToken = webToken,
                };
            }
        }
    }
}