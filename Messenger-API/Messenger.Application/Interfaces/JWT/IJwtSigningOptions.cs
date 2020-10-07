using Microsoft.IdentityModel.Tokens;

namespace Messenger.Application.Interfaces.JWT
{
    public interface IJwtSigningOptions
    {
        SigningCredentials SigningCredentials { get; }
    }
}