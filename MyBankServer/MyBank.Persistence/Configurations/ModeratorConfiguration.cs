using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class ModeratorConfiguration : IEntityTypeConfiguration<ModeratorEntity>
{
    public void Configure(EntityTypeBuilder<ModeratorEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .HasMany(m => m.Messages)
            .WithOne(m => m.SenderModerator)
            .HasForeignKey(m => m.SenderModeratorId);

        builder
            .HasMany(m => m.CreditRequestsReplied)
            .WithOne(cr => cr.Moderator)
            .HasForeignKey(cr => cr.ModeratorId)
            .IsRequired(false);

        builder
            .HasMany(m => m.CreditsApproved)
            .WithOne(ca => ca.ModeratorApproved)
            .HasForeignKey(ca => ca.ModeratorApprovedId);
    }
}
