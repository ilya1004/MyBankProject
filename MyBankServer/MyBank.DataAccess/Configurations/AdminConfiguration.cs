using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<AdminEntity>
{
    public void Configure(EntityTypeBuilder<AdminEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .HasMany(a => a.Messages)
            .WithOne(m => m.SenderAdmin);
    }

}
