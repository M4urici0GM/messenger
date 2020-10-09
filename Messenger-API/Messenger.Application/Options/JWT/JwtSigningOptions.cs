using System.Security.Cryptography;
using System.Text;
using FluentValidation.Validators;
using Messenger.Application.Interfaces.JWT;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Application.Options.JWT
{
    public class JwtSigningOptions : IJwtSigningOptions
    {
        public SigningCredentials SigningCredentials { get; }
        
        public JwtSigningOptions(IJwtOptions jwtOptions)
        {
            byte[] key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}