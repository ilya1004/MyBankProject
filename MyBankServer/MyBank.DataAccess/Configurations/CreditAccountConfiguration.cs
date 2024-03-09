using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess.Configurations;

public class CreditAccountConfiguration : IEntityTypeConfiguration<CreditAccountEntity>
{
    public void Configure(EntityTypeBuilder<CreditAccountEntity> builder)
    {
        builder.HasKey(ca => ca.Id);

        builder
            .HasOne(ca => ca.UserOwner)
            .WithMany(u => u.CreditAccounts)
            .HasForeignKey(ca => ca.UserId);

        builder
            .HasOne(ca => ca.Currency)
            .WithMany(c => c.CreditAccounts)
            .HasForeignKey(ca => ca.CurrencyId);
            
        builder
            .HasOne(ca => ca.ModeratorApproved)
            .WithMany(m => m.CreditsApproved)
            .HasForeignKey(ca => ca.ModeratorApprovedId);

        builder
            .HasOne(ca => ca.Card)
            .WithOne(c => c.CreditAccount);

        builder
            .HasMany(ca => ca.Payments)
            .WithOne(p => p.CreditAccount)
            .HasForeignKey(p => p.CreditAccountId);

        builder
            .HasMany(ca => ca.Transactions)
            .WithOne(t => t.CreditAccount)
            .HasForeignKey(t => t.CreditAccountId);
    }
}
