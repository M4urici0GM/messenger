using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Security;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Application.Contexts.Authentication.Commands
{
    public class GenerateToken : IRequest<WebToken>
    {
        public List<Claim> Claims { get; set; } = new List<Claim>();

        public class GenerateTokenHandler : IRequestHandler<GenerateToken, WebToken>
        {
            private readonly IMainDbContext _mainDbContext;
            private readonly IMapper _mapper;
            private readonly JwtSecurityTokenHandler _tokenHandler;
            private readonly ITokenConfiguration _tokenConfiguration;
            private readonly ISignInConfiguration _signInConfiguration;

            public GenerateTokenHandler(IMainDbContext mainDbContext, IMapper mapper,
                ITokenConfiguration tokenConfiguration, ISignInConfiguration signInConfiguration)
            {
                _mainDbContext = mainDbContext;
                _mapper = mapper;
                _tokenHandler = new JwtSecurityTokenHandler();
                _tokenConfiguration = tokenConfiguration;
                _signInConfiguration = signInConfiguration;
            }

            public async Task<WebToken> Handle(GenerateToken request, CancellationToken cancellationToken)
            {
                List<Claim> claimList = request.Claims;
                claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
                
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(Guid.NewGuid().ToString(), "TokenIdentifier"), claimList);

                DateTime expirationDate = DateTime.Now.AddSeconds(_tokenConfiguration.ExpiresIn);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _tokenConfiguration.Audience,
                    Issuer = _tokenConfiguration.Issuer,
                    Expires = expirationDate,
                    NotBefore = DateTime.Now,
                    Subject = identity,
                    SigningCredentials = _signInConfiguration.SigningCredentials,
                };

                SecurityToken securityToken = _tokenHandler.CreateToken(tokenDescriptor);
                
                return new WebToken
                {
                    Token = _tokenHandler.WriteToken(securityToken),
                    CreatedAt = DateTime.Now,
                    ExpirationDate = expirationDate,
                };
            }
        }
    }
}