using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Database.Entities;

namespace MyBank.Database.Configurations;

public class CardPackageConfiguration : IEntityTypeConfiguration<CardPackageEntity>
{
    public void Configure(EntityTypeBuilder<CardPackageEntity> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder
            .HasMany(cp => cp.Cards)
            .WithOne(c => c.CardPackage)
            .HasForeignKey(c => c.CardPackageId);
    }
}
