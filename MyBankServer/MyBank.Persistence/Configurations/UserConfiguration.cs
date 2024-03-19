using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasMany(u => u.PersonalAccounts)
            .WithOne(pa => pa.UserOwner)
            .HasForeignKey(pa => pa.UserId);

        builder
            .HasMany(u => u.CreditAccounts)
            .WithOne(ca => ca.UserOwner)
            .HasForeignKey(ca => ca.UserId);

        builder
            .HasMany(u => u.DepositAccounts)
            .WithOne(da => da.UserOwner)
            .HasForeignKey(da => da.UserId);

        builder
            .HasMany(u => u.Cards)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        builder
            .HasMany(u => u.CreditRequests)
            .WithOne(cr => cr.User)
            .HasForeignKey(cr => cr.UserId);

        builder
            .HasMany(u => u.CreditPayments)
            .WithOne(cp => cp.User)
            .HasForeignKey(cp => cp.UserId);

        builder
            .HasMany(u => u.Messages)
            .WithOne(m => m.SenderUser)
            .HasForeignKey(m => m.SenderUserId);

    }
}