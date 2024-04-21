using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class CreditRequestConfiguration : IEntityTypeConfiguration<CreditRequestEntity>
{
    public void Configure(EntityTypeBuilder<CreditRequestEntity> builder)
    {
        builder.HasKey(cr => cr.Id);
    }
}
