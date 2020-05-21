using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence.Contexts
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public DbSet<User> Users { get; set; }
        
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            DateTime now = DateTime.Now;

            ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Added)
                .ToList()
                .ForEach(e =>
                {
                    e.Property(p => p.CreatedAt).CurrentValue = now;
                    e.Property(p => p.UpdatedAt).CurrentValue = now;
                });
            
            ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Modified)
                .ToList()
                .ForEach(e =>
                {
                    e.Property(p => p.UpdatedAt).CurrentValue = now;
                });
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}