using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Messenger.Domain.Entities;
using Messenger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Persistence.Context
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            :base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            DateTime now = DateTime.UtcNow;

            ChangeTracker.Entries<IEntity>()
                .Where(x => x.State == EntityState.Added)
                .ToList()
                .ForEach(x =>
                {
                    x.Property(y => y.CreatedAt).CurrentValue = now;
                    x.Property(y => y.UpdatedAt).CurrentValue = now;
                });
            
            ChangeTracker.Entries<IEntity>()
                .Where(x => x.State == EntityState.Modified)
                .ToList()
                .ForEach(x => x.Property(y => y.UpdatedAt).CurrentValue = now);
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}