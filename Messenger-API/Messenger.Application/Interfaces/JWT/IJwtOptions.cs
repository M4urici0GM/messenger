namespace Messenger.Application.Interfaces.JWT
{
    public interface IJwtOptions
    {
        string Audience { get; set; }
        string Issuer { get; set; }
        int ExpiresIn { get; set; }
        string SecretKey { get; set; }
        int RefreshTokenExpiresIn { get; set; }
    }
}