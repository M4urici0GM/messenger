using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class VerificationTokenConfiguration : IEntityTypeConfiguration<VerificationToken>
    {
        public void Configure(EntityTypeBuilder<VerificationToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}