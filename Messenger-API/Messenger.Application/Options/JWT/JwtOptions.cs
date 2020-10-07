using Messenger.Application.Interfaces.JWT;

namespace Messenger.Application.Options.JWT
{
    public class JwtOptions : IJwtOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiresIn { get; set; } // The Expiration time in seconds.
        public string SecretKey { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}