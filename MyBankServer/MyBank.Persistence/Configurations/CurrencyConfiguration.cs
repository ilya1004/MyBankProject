using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyEntity>
{
    public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasMany(c => c.PersonalAccounts)
            .WithOne(pa => pa.Currency)
            .HasForeignKey(pa => pa.CurrencyId);

        builder
            .HasMany(c => c.CreditPackages)
            .WithOne(cp => cp.Currency)
            .HasForeignKey(cp => cp.CurrencyId);

        builder
            .HasMany(c => c.CreditAccounts)
            .WithOne(ca => ca.Currency)
            .HasForeignKey(ca => ca.CurrencyId);

        builder
            .HasMany(c => c.DepositPackages)
            .WithOne(dp => dp.Currency)
            .HasForeignKey(dp => dp.CurrencyId);

        builder
            .HasMany(c => c.DepositAccounts)
            .WithOne(da => da.Currency)
            .HasForeignKey(da => da.CurrencyId);
    }
}
