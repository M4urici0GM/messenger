using System.Threading.Tasks;

namespace Application.Interfaces.Security
{
    public interface IEncryptService
    {
        Task<string> HashPassword(string password);
        Task<string> HashPassword(string password, string salt);
        Task<bool> VerifyHash(string password, string hash);
        Task<string> GenerateSalt();
    }
}