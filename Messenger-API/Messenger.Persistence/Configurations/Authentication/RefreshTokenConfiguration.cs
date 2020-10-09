using System;
using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Persistence.Configurations.Authentication
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(time => time, time => DateTime.SpecifyKind(time, DateTimeKind.Utc));
            
            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasConversion(time => time, time => DateTime.SpecifyKind(time, DateTimeKind.Utc));
        }
    }
}