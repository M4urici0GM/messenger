
using Application.Interfaces.Configurations;

namespace Application.Configurations
{
    public class AppConfiguration : IAppConfiguration
    {
        public int VerificationTokenExpiresIn { get; set; }
    }
}