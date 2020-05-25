using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.To)
                .WithMany()
                .HasForeignKey(p => p.UserToId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.From)
                .WithMany()
                .HasForeignKey(p => p.UserFromId)
                .OnDelete(DeleteBehavior.NoAction);;

            builder.HasOne(p => p.Message)
                .WithMany()
                .HasForeignKey(p => p.MessageId)
                .OnDelete(DeleteBehavior.NoAction);;
        }
    }
}