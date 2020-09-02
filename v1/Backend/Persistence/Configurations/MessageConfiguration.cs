using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Content)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);
            builder.Property(p => p.IsSeen)
                .HasDefaultValue(false);
            builder.Property(p => p.MimeType)
                .IsRequired();
            builder.Property(p => p.SeenDate)
                .IsRequired();
        }
    }
}