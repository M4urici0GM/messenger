﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
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
            
            public CreateUserHandler(IMainDbContext mainDbContext, IMapper mapper)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
            }
            
            public async Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                CreateUserValidator validator = new CreateUserValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    throw new EntityValidationException(nameof(CreateUser), request, validationResult.Errors);

                User currentUser = await _mainDbContext.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                if (currentUser != null)
                    throw new EntityAlreadyExists(nameof(User), request.Email);
                
                User newUser = _mapper.Map<User>(request);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                await _mainDbContext.Users.AddAsync(newUser, cancellationToken);
                await _mainDbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<UserDto>(newUser);
            }
        }
    }
}