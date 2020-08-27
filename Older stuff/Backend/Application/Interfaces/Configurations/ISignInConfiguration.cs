using Microsoft.IdentityModel.Tokens;

namespace Application.Interfaces.Configurations
{
    public interface ISignInConfiguration
    {
        SigningCredentials SigningCredentials { get; }
        SecurityKey SecurityKey { get; }
    }
}