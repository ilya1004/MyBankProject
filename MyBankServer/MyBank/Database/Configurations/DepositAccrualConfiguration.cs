﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Database.Entities;

namespace MyBank.Database.Configurations;

public class DepositAccrualConfiguration : IEntityTypeConfiguration<DepositAccrualEntity>
{
    public void Configure(EntityTypeBuilder<DepositAccrualEntity> builder)
    {
        builder.HasKey(da => da.Id);

        builder
            .HasOne(da => da.DepositAccount)
            .WithMany(da => da.Accruals)
            .HasForeignKey(da => da.DepositAccountId);
    }
}
