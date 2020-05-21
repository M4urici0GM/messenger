using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public DbSet<User> Users { get; set; }
        
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {}
    }
}