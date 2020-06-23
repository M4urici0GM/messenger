namespace Application.Interfaces.Configurations
{
    public interface IAppConfiguration
    {
        int VerificationTokenExpiresIn { get; set; }
    }
}