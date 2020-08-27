using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IMainDbContext 
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<UserMessage> UserMessages { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<VerificationToken> VerificationTokens { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}