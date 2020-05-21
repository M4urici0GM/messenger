using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IMainDbContext 
    {
        DbSet<User> Users { get; set; }
    }
}