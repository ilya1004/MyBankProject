using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Database.Entities;

namespace MyBank.Database.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder
            .HasOne(t => t.PersonalAccount)
            .WithMany(pa => pa.Transactions)
            .HasForeignKey(t => t.PersonalAccountId);

        builder
            .HasOne(t => t.CreditAccount)
            .WithMany(pa => pa.Transactions)
            .HasForeignKey(t => t.CreditAccountId);
    }
}
