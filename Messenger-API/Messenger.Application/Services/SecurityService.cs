using System.Threading.Tasks;
using Messenger.Application.Interfaces;

namespace Messenger.Application.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return Task.FromResult(hashedPassword);
        }
    }
}