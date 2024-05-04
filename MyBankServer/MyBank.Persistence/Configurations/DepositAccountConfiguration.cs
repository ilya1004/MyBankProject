using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBank.Persistence.Configurations;

public class DepositAccountConfiguration : IEntityTypeConfiguration<DepositAccountEntity>
{
    public void Configure(EntityTypeBuilder<DepositAccountEntity> builder)
    {
        builder.HasKey(ca => ca.Id);

        builder
            .HasMany(ca => ca.Accruals)
            .WithOne(a => a.DepositAccount)
            .HasForeignKey(a => a.DepositAccountId);
    }
}