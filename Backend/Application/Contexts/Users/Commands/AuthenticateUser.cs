using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Application.Contexts.Users.Validators;
using Application.DataTransferObjects;
using Application.Interfaces;
using Application.Interfaces.Security;
using Application.Security.WebToken;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Application.Contexts.Users.Commands
{
    public class AuthenticateUser : IRequest<AuthenticatedUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, AuthenticatedUserDto>
        {

            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            private readonly ITokenConfiguration _tokenConfiguration;
            private readonly ISignInConfiguration _signInConfiguration;
            
            public AuthenticateUserHandler(IMainDbContext mainDbContext, IMapper mapper, ITokenConfiguration tokenConfiguration, ISignInConfiguration signInConfiguration)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
                _tokenConfiguration = tokenConfiguration;
                _signInConfiguration = signInConfiguration;
            }
            
            public async Task<AuthenticatedUserDto> Handle(AuthenticateUser request, CancellationToken cancellationToken)
            {
                AuthenticateUserValidator validator = new AuthenticateUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(AuthenticateUser), request, validationResult.Errors);

                User user = await _mainDbContext.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
                
                if (user.Equals(null))
                    throw new InvalidCredentialException();

                string passwordHash = user.Password;
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, passwordHash);

                if (!isPasswordValid)
                    throw new InvalidCredentialException();

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
                ClaimsIdentity identity = new ClaimsIdentity(claims);
                JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();

                DateTime expirationDate = new DateTime().AddSeconds(_tokenConfiguration.ExpiresIn);
                
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _tokenConfiguration.Audience,
                    Issuer = _tokenConfiguration.Issuer,
                    Expires = expirationDate,
                    Subject = identity,
                    SigningCredentials = _signInConfiguration.SigningCredentials,
                    CompressionAlgorithm = CompressionAlgorithms.Deflate,
                };
                
                return new AuthenticatedUserDto
                {
                    User = _mapper.Map<UserDto>(user),
                    WebToken = new WebToken
                    {
                        CreatedAt = DateTime.Now,
                        ExpirationDate = expirationDate,
                        Token = tokenHandler.CreateToken(tokenDescriptor),
                    }
                };
            }
        }
    }
}