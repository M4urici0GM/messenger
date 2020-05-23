using System.Threading;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.Interfaces;
using AutoMapper;
using MediatR;

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
            
            public Task<AuthenticatedUserDto> Handle(AuthenticateUser request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}