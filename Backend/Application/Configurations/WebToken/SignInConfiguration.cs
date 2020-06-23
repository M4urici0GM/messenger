using System.Text;
using Application.Interfaces.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Application.Configurations.WebToken
{
    public class SignInConfiguration : ISignInConfiguration
    {
        public SigningCredentials SigningCredentials { get; }
        public SecurityKey SecurityKey { get; }
        
        public SignInConfiguration(ITokenConfiguration tokenConfiguration)
        {
            byte[] key = Encoding.UTF8.GetBytes(tokenConfiguration.SecurityKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityKey = securityKey;
            
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}