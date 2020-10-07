using System;
using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Persistence.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(time => time, time => DateTime.SpecifyKind(time, DateTimeKind.Utc));

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasConversion(time => time, time => DateTime.SpecifyKind(time, DateTimeKind.Utc));

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

        }
    }
}