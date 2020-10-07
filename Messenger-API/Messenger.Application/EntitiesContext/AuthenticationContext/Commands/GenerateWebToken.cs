using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Application.Interfaces.JWT;
using Messenger.Domain.Entities;
using Messenger.Domain.Structs;
using Messenger.Persistence.Repositories.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Application.EntitiesContext.AuthenticationContext.Commands
{
    public class GenerateWebToken : IRequest<WebToken>
    {
        public User User { get; set; }
        public List<Claim> Claims { get; set; } =  new List<Claim>();
        
        public class GenerateWebTokenHandler : IRequestHandler<GenerateWebToken, WebToken>
        {

            private readonly IJwtSigningOptions _jwtSigningOptions;
            private readonly IJwtOptions _jwtOptions;
            private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            
            public GenerateWebTokenHandler(IJwtSigningOptions jwtSigningOptions, IJwtOptions jwtOptions, IRefreshTokenRepository refreshTokenRepository)
            {
                _jwtSigningOptions = jwtSigningOptions;
                _jwtOptions = jwtOptions;
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                _refreshTokenRepository = refreshTokenRepository;
            }
            
            public async Task<WebToken> Handle(GenerateWebToken request, CancellationToken cancellationToken)
            {
                if (request.User == null)
                    throw new InvalidCredentialException("");

                DateTime authTime = DateTime.UtcNow;
                DateTime expirationDate = authTime.AddSeconds(_jwtOptions.ExpiresIn);
                
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtClaimNames.UserId, request.User.Id.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, request.User.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.AuthTime, authTime.ToString("u")));
                claims.Add(new Claim("Name", $"{request.User.FirstName} {request.User.LastName}"));
                
                claims.AddRange(request.Claims);

                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(request.User.Id.ToString()), claims);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _jwtOptions.Audience,
                    Issuer = _jwtOptions.Issuer,
                    Expires = expirationDate,
                    NotBefore = authTime,
                    Subject = identity,
                    SigningCredentials = _jwtSigningOptions.SigningCredentials,
                    IssuedAt = authTime,
                };

                RefreshToken refreshToken = new RefreshToken
                {
                    UserId = request.User.Id,
                    Token = Guid.NewGuid(),
                    ExpirationDate = authTime.AddSeconds(_jwtOptions.RefreshTokenExpiresIn),
                    IsActive = true,
                };

                await _refreshTokenRepository.Add(refreshToken, cancellationToken);

                SecurityToken securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
                return new WebToken
                {
                    Token = _jwtSecurityTokenHandler.WriteToken(securityToken),
                    ExpirationDate = expirationDate,
                    RefreshToken = refreshToken,
                    CreatedAt = DateTime.UtcNow,
                };
            }
        }
        
    }
}