using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class DepositAccountConfiguration : IEntityTypeConfiguration<DepositAccountEntity>
{
    public void Configure(EntityTypeBuilder<DepositAccountEntity> builder)
    {
        builder.HasKey(ca => ca.Id);

        //builder
        //    .HasOne(ca => ca.UserOwner)
        //    .WithMany(u => u.DepositAccounts)
        //    .HasForeignKey(ca => ca.UserId);

        builder
            .HasOne(ca => ca.Currency)
            .WithMany(c => c.DepositAccounts)
            .HasForeignKey(ca => ca.CurrencyId);

        builder
            .HasMany(ca => ca.Accruals)
            .WithOne(a => a.DepositAccount)
            .HasForeignKey(a => a.DepositAccountId);
    }
}
