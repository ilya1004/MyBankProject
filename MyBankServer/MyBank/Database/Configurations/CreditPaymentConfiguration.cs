﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Database.Entities;

namespace MyBank.Database.Configurations;

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
