﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Configurations;

public class PersonalAccountConfiguration : IEntityTypeConfiguration<PersonalAccountEntity>
{
    public void Configure(EntityTypeBuilder<PersonalAccountEntity> builder)
    {
        builder.HasKey(pa => pa.Id);

        builder
            .HasMany(pa => pa.Cards)
            .WithOne(c => c.PersonalAccount)
            .HasForeignKey(c => c.PersonalAccountId);
    }
}
