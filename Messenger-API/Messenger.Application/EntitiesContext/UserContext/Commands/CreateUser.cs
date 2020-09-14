using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.EntitiesContext.UserContext.Validators;
using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Domain.Interfaces;
using Newtonsoft.Json;

namespace Messenger.Application.EntitiesContext.UserContext.Commands
{
    public class CreateUser : IRequest<UserDto>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
        {
            private readonly IMainDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly ISecurityService _securityService;
            
            public CreateUserHandler(IMainDbContext mainDbContext, IMapper mapper, ISecurityService securityService)
            {
                _dbContext = mainDbContext;
                _mapper = mapper;
                _securityService = securityService;
            }
            
            public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                CreateUserValidator validator = new CreateUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
    
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(User), request, validationResult.Errors);

                User user = _mapper.Map<User>(request);
                user.Password = await _securityService.HashPassword(request.Password);
                
                await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<UserDto>(user);
            }
        }
    }
}