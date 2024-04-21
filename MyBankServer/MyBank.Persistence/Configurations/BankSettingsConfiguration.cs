using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBank.Persistence.Configurations;

public class BankSettingsConfiguration : IEntityTypeConfiguration<BankSettingsEntity>
{
    public void Configure(EntityTypeBuilder<BankSettingsEntity> builder)
    {
        builder.HasKey(bs => bs.Id);
    }
}
