using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Contexts.Users.Notifications;
using Application.Contexts.Users.Validators;
using Application.DataTransferObjects;
using Application.Interfaces;
using Application.Interfaces.Security;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Contexts.Users.Commands
{
    public class CreateUser : IRequest<UserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
        {
            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            private readonly ILogger<CreateUserHandler> _logger;
            private readonly IEncryptService _encryptService;
            private readonly IMediator _mediator;

            public CreateUserHandler(IMainDbContext mainDbContext, IMapper mapper, ILogger<CreateUserHandler> logger,
                IEncryptService encryptService, IMediator mediator)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
                _encryptService = encryptService;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                CreateUserValidator validator = new CreateUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(CreateUser), request, validationResult.Errors);

                User currentUser = await _mainDbContext.Users
                    .AsNoTracking()
                    .Where(u => u.Email == request.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (currentUser != null)
                    throw new EntityAlreadyExists(nameof(User), request.Email);

                User newUser = _mapper.Map<User>(request);
                newUser.Password = await _encryptService.HashPassword(request.Password);

                await _mainDbContext.Users.AddAsync(newUser, cancellationToken);
                await _mainDbContext.SaveChangesAsync(cancellationToken);

                _logger.LogDebug($"Created user: {request.Email}");
                await _mediator.Publish(new UserCreated {User = newUser});
                return _mapper.Map<UserDto>(newUser);
            }
        }
    }
}