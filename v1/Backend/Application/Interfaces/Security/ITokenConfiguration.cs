namespace Application.Interfaces.Security
{
    public interface ITokenConfiguration
    {
        string Audience { get; set; }
        string Issuer { get; set; }
        int ExpiresIn { get; set; }
        string SecurityKey { get; set; }
    }
}