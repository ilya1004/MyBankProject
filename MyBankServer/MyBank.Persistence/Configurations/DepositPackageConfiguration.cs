using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBank.Persistence.Configurations;

public class DepositPackageConfiguration : IEntityTypeConfiguration<DepositPackageEntity>
{
    public void Configure(EntityTypeBuilder<DepositPackageEntity> builder)
    {
        builder.HasKey(dp => dp.Id);

    }
}
