using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Messenger.Application.DataTransferObjects.Users;
using Messenger.Application.EntitiesContext.AuthenticationContext.Commands;
using Messenger.Domain.Entities;
using Messenger.Persistence.Repositories.Interfaces.Security;
using Messenger.Persistence.Repositories.Interfaces.Users;
using MongoDB.Driver;

namespace Messenger.Application.EntitiesContext.UserContext.Commands
{
    public class AuthWithRefreshToken : IRequest<AuthenticatedUserDto>
    {
        public Guid Token { get; set; }

        public class AuthWithRefreshTokenHandler : IRequestHandler<AuthWithRefreshToken, AuthenticatedUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public AuthWithRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IMediator mediator,
                IUserRepository userRepository, IMapper mapper)
            {
                _refreshTokenRepository = refreshTokenRepository;
                _mediator = mediator;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<AuthenticatedUserDto> Handle(AuthWithRefreshToken request,
                CancellationToken cancellationToken)
            {
                if (request.Token == Guid.Empty)
                    throw new InvalidCredentialException("Invalid grant type");

                FilterDefinition<RefreshToken> filter = Builders<RefreshToken>.Filter
                    .And(new FilterDefinition<RefreshToken>[]
                    {
                        Builders<RefreshToken>.Filter.Eq(x => x.Token, request.Token),
                        Builders<RefreshToken>.Filter.Eq(x => x.IsActive, true),
                        Builders<RefreshToken>.Filter.Lte(x => x.ExpirationDate, DateTime.UtcNow)
                    });

                RefreshToken refreshToken = await _refreshTokenRepository.Get(filter, cancellationToken);
                if (refreshToken == null)
                    throw new InvalidCredentialException("Invalid Credentials");

                User user = await _userRepository.Get(refreshToken.UserId, cancellationToken);
                if (user == null)
                    throw new InvalidCredentialException("Invalid Credentials");

                try
                {
                    WebToken webToken = await _mediator.Send(new GenerateWebToken() {User = user}, cancellationToken);

                    refreshToken.IsActive = false;
                    await _refreshTokenRepository.Update(refreshToken, cancellationToken);
                    await _refreshTokenRepository.Delete(Builders<RefreshToken>.Filter.Eq(x => x.IsActive, false),
                        cancellationToken);

                    return new AuthenticatedUserDto
                    {
                        Token = webToken,
                        User = _mapper.Map<UserDto>(user),
                    };
                }
                catch (Exception ex)
                {
                    throw new InvalidCredentialException("", ex);
                }
            }
        }
    }
}