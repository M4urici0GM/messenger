using Microsoft.IdentityModel.Tokens;

namespace Application.Interfaces.Security
{
    public interface ISignInConfiguration
    {
        SigningCredentials SigningCredentials { get; }
    }
}