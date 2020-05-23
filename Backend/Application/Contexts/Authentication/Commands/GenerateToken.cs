using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Authentication.Commands
{
    public class GenerateToken : IRequest<WebToken>
    {
        public User User { get; set; }
        
        public class GenerateTokenHandler : IRequestHandler<GenerateToken, WebToken>
        {

            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;

            public GenerateTokenHandler(IMainDbContext mainDbContext, IMapper mapper)
            {
                
            }
            
            public Task<WebToken> Handle(GenerateToken request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}