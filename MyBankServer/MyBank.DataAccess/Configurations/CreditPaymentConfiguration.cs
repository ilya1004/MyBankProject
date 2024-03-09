using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.DataAccess.Entities;
using System.Runtime.ConstrainedExecution;

namespace MyBank.DataAccess.Configurations;

public class CreditPaymentConfiguration : IEntityTypeConfiguration<CreditPaymentEntity>
{
    public void Configure(EntityTypeBuilder<CreditPaymentEntity> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder
            .HasOne(cp => cp.CreditAccount)
            .WithMany(ca => ca.Payments)
            .HasForeignKey(cp => cp.CreditAccountId);

        builder
            .HasOne(cp => cp.User)
            .WithMany(u => u.CreditPayments)
            .HasForeignKey(cp => cp.UserId);
    }
}
