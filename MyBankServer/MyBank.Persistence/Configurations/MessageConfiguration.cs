using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .HasOne(m => m.SenderUser)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderUserId)
            .IsRequired(false);

        builder
            .HasOne(m => m.SenderModerator)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderModeratorId)
            .IsRequired(false);

        builder
            .HasOne(m => m.SenderAdmin)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderAdminId)
            .IsRequired(false);
    }
}
