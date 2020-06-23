using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasKey(x => new { x.Id, x.MessageId, x.UserToId, x.UserFromId });

            builder
                .HasOne(x => x.Message)
                .WithMany(x => x.UserMessages)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.From)
                .WithMany(x => x.ReceivedMessages)
                .HasForeignKey(x => x.UserFromId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.To)
                .WithMany(x => x.SentMessages)
                .HasForeignKey(x => x.UserToId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}