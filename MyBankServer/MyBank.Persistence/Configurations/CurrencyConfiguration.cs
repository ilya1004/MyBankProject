using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyEntity>
{
    public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.CreditRequests)
            .WithOne(cr => cr.Currency)
            .HasForeignKey(c => c.CurrencyId);
    }
}
