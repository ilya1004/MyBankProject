using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBank.Persistence.Configurations;

public class CreditPackageConfiguration : IEntityTypeConfiguration<CreditPackageEntity>
{
    public void Configure(EntityTypeBuilder<CreditPackageEntity> builder)
    {
        builder.HasKey(cp => cp.Id);
    }
}