using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contexts.Users.Validators;
using Application.DataTransferObjects;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts.Users.Commands
{
    public class DeleteUser : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
        public class DeleteUserHandler : IRequestHandler<DeleteUser, UserDto>
        {
            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            
            public DeleteUserHandler(IMainDbContext mainDbContext, IMapper mapper)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
            }
            
            public async Task<UserDto> Handle(DeleteUser request, CancellationToken cancellationToken)
            {
                DeleteUserValidator validator = new DeleteUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(DeleteUser), request.UserId, validationResult.Errors);

                User user = await _mainDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId,cancellationToken);
                
                if (user.Equals(null))
                    throw new EntityNotFoundException(nameof(User), request.UserId);

                user.IsDeleted = true;
                
                await _mainDbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<UserDto>(user);
            }
        }
    }
}