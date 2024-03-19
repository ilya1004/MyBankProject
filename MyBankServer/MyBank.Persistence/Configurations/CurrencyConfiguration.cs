
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
            .HasForeignKey(c => c.CurrencyId);

        builder
            .HasMany(c => c.CreditAccounts)
            .WithOne(pa => pa.Currency)
            .HasForeignKey(c => c.CurrencyId);

        builder
            .HasMany(c => c.DepositAccounts)
            .WithOne(da => da.Currency)
            .HasForeignKey(c => c.CurrencyId);
    }
}
