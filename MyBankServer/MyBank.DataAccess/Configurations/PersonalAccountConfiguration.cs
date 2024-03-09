using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess.Configurations;

public class PersonalAccountConfiguration : IEntityTypeConfiguration<PersonalAccountEntity>
{
    public void Configure(EntityTypeBuilder<PersonalAccountEntity> builder)
    {
        builder.HasKey(pa => pa.Id);

        builder
            .HasOne(pa => pa.UserOwner)
            .WithMany(u => u.PersonalAccounts)
            .HasForeignKey(pa => pa.UserId);

        builder
            .HasOne(pa => pa.Currency)
            .WithMany(c => c.PersonalAccounts)
            .HasForeignKey(pa => pa.CurrencyId);

        builder
            .HasMany(pa => pa.Transactions)
            .WithOne(t => t.PersonalAccount)
            .HasForeignKey(t => t.PersonalAccountId);

        builder
            .HasMany(pa => pa.Cards)
            .WithOne(c => c.PersonalAccount)
            .HasForeignKey(c => c.PersonalAccountId);
    }
}
