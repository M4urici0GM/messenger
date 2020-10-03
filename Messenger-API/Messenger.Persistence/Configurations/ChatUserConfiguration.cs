using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Persistence.Configurations
{
    public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsAdmin).HasDefaultValue(false);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}