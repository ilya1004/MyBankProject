using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CreditAccountConfiguration : IEntityTypeConfiguration<CreditAccountEntity>
{
    public void Configure(EntityTypeBuilder<CreditAccountEntity> builder)
    {
        builder.HasKey(ca => ca.Id);

        builder
            .HasMany(ca => ca.Payments)
            .WithOne(p => p.CreditAccount)
            .HasForeignKey(p => p.CreditAccountId);
    }
}
