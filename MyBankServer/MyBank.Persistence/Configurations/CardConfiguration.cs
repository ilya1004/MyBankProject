using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasOne(c => c.CardPackage)
            .WithMany(cp => cp.Cards)
            .HasForeignKey(c => c.CardPackageId);

        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Cards)
            .HasForeignKey(c => c.UserId);

        builder
            .HasOne(c => c.PersonalAccount)
            .WithMany(pa => pa.Cards)
            .HasForeignKey(c => c.PersonalAccountId);
    }
}
