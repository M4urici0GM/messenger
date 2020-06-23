using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasAlternateKey(p => p.Email);
            
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Email)
                .IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.IsVerified)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}