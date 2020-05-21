using System;
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
    public class UpdateUserInfo : IRequest<UserDto>
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }

        public class UpdateUserHandler : IRequestHandler<UpdateUserInfo, UserDto>
        {

            private readonly IMapper _mapper;
            private readonly IMainDbContext _mainDbContext;

            public UpdateUserHandler(IMainDbContext mainDbContext, IMapper mapper)
            {
                _mapper = mapper;
                _mainDbContext = mainDbContext;
            }
            
            public async Task<UserDto> Handle(UpdateUserInfo request, CancellationToken cancellationToken)
            {
                var validator = new UpdateUserInfoValidator();
                ValidationResult validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(UpdateUserInfo), request, validationResult.Errors);

                User user = await _mainDbContext.Users
                    .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);
                
                if (user == null)
                    throw new EntityNotFoundException(nameof(User), request.UserId);

                if (!string.IsNullOrEmpty(request.NewPassword) && !string.IsNullOrEmpty(request.CurrentPassword))
                {
                    bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
                    if (!isCurrentPasswordValid)
                        throw new InvalidCredentialException();
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                }
                
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;

                await _mainDbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<UserDto>(user);
            }
        }
    }
}