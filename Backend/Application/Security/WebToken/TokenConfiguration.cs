using Application.Interfaces.Security;

namespace Application.Security.WebToken
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiresIn { get; set; }
        public string SecurityKey { get; set; }
    }
}