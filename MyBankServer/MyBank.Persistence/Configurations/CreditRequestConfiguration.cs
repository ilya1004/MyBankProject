using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CreditRequestConfiguration : IEntityTypeConfiguration<CreditRequestEntity>
{
    public void Configure(EntityTypeBuilder<CreditRequestEntity> builder)
    {
        builder.HasKey(cr => cr.Id);

        //builder
        //    .HasOne(cr => cr.Moderator)
        //    .WithMany(m => m.CreditRequestsReplied)
        //    .HasForeignKey(cr => cr.ModeratorId);

        //builder
        //    .HasOne(cr => cr.User)
        //    .WithMany(u => u.CreditRequests)
        //    .HasForeignKey(cr => cr.UserId);
    }
}