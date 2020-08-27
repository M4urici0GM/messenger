using System.Threading.Tasks;
using Application.Interfaces.Security;

namespace Application.Services.Security
{
    public class EncryptService : IEncryptService
    {
        public Task<string> HashPassword(string password)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }

        public Task<string> HashPassword(string password, string salt)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password, salt));
        }

        public Task<bool> VerifyHash(string password, string hash)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, hash));
        }

        public Task<string> GenerateSalt()
        {
            return Task.FromResult(BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
}