using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Messenger.Application.DataTransferObjects;
using Messenger.Application.DataTransferObjects.Users;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Domain.Interfaces;
using Messenger.Persistence.Repositories.Interfaces.Users;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Messenger.Application.EntitiesContext.UserContext.Commands
{
    public class GetUser : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public class GetUserHandler : IRequestHandler<GetUser, UserDto>
        {

            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
        
            public GetUserHandler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }
        
            public async Task<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
            {
                FilterDefinition<User> filter = Builders<User>.Filter
                    .Eq(x => x.Id, request.Id);

                User user = await _userRepository.Get(filter, cancellationToken);

                if (user == null)
                    throw new EntityNotFoundException(nameof(User), request.Id);

                return _mapper.Map<UserDto>(user);
            }
        }
    }
}