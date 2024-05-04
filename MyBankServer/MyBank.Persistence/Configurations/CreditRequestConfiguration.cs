using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBank.Persistence.Configurations;

public class CreditRequestConfiguration : IEntityTypeConfiguration<CreditRequestEntity>
{
    public void Configure(EntityTypeBuilder<CreditRequestEntity> builder)
    {
        builder.HasKey(cr => cr.Id);

        builder
            .HasOne(cr => cr.CreditPackage)
            .WithMany(cp => cp.CreditRequests)
            .HasForeignKey(cr => cr.CreditPackageId);
    }
}
