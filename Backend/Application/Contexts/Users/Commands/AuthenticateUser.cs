using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Contexts.Users.Validators;
using Application.DataTransferObjects;
using Application.Interfaces;
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
        
        public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, AuthenticatedUserDto>
        {

            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            
            public AuthenticateUserHandler(IMainDbContext mainDbContext, IMapper mapper)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
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
                
                
            }
        }
    }
}