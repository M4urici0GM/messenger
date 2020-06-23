using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts.Users.Queries
{
    public class GetCurrentUser : IRequest<UserDto>
    {

        public class GetCurrentUserHandler : IRequestHandler<GetCurrentUser, UserDto>
        {
            private readonly IMainDbContext _mainDbContext;
            private readonly IHttpContextAccessor _httpContext;
            private readonly IMapper _mapper;
            
            public GetCurrentUserHandler(IMainDbContext dbContext, IHttpContextAccessor httpContext, IMapper mapper)
            {
                _mainDbContext = dbContext;
                _httpContext = httpContext;
                _mapper = mapper;
            }
            
            public async Task<UserDto> Handle(GetCurrentUser request, CancellationToken cancellationToken)
            {
                ClaimsIdentity claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                Claim claim = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == "UserId");
    
                string UserId = claim?.Value ?? string.Empty;
                if (string.IsNullOrEmpty(UserId))
                    throw new EntityNotFoundException(nameof(User), "Current Authenticated user");

                Guid id = Guid.Parse(UserId);
                User user = await _mainDbContext.Users
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                if (user == null)
                    throw new EntityNotFoundException(nameof(User), id);

                return _mapper.Map<UserDto>(user);
            }
        }
        
    }
}