using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Messenger.Application.DataTransferObjects;
using Messenger.Domain.Entities;
using Messenger.Domain.Exceptions;
using Messenger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Application.EntitiesContext.UserContext.Commands
{
    public class GetUser : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public class GetUserHandler : IRequestHandler<GetUser, UserDto>
        {

            private readonly IMapper _mapper;
            private readonly IMainDbContext _mainDbContext;
        
            public GetUserHandler(IMapper mapper, IMainDbContext mainDbContext)
            {
                _mapper = mapper;
                _mainDbContext = mainDbContext;
            }
        
            public async Task<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
            {
                User user = await _mainDbContext
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
                if (user == null)
                    throw new EntityNotFoundException(nameof(User), request.Id);

                return _mapper.Map<UserDto>(user);
            }
        }
    }
}