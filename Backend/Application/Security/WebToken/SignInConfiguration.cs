using System.Text;
using Application.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace Application.Security.WebToken
{
    public class SignInConfiguration : ISignInConfiguration
    {
        public SigningCredentials SigningCredentials { get; }
        
        public SignInConfiguration(ITokenConfiguration tokenConfiguration)
        {
            byte[] key = Encoding.UTF8.GetBytes(tokenConfiguration.SecurityKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}