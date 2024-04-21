using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CreditPaymentConfiguration : IEntityTypeConfiguration<CreditPaymentEntity>
{
    public void Configure(EntityTypeBuilder<CreditPaymentEntity> builder)
    {
        builder.HasKey(cp => cp.Id);
    }
}
