using Microsoft.EntityFrameworkCore;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess;

public class MyBankDbContext : DbContext
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<CardPackageEntity> CardPackages { get; set; }
    public DbSet<CreditAccountEntity> Credits { get; set; }
    public DbSet<CreditPaymentEntity> CreditPayments { get; set; }
    public DbSet<CreditRequestEntity> CreditRequests { get; set; }
    public DbSet<CurrencyEntity> Currencies { get; set; }
    public DbSet<DepositAccountEntity> Deposits { get; set; }
    public DbSet<DepositAccrualEntity> DepositAccruals { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<ModeratorEntity> Moderators { get; set; }
    public DbSet<PersonalAccountEntity> PersonalAccounts { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardEntity>()
            .HasOne(c => c.PersonalAccount)
            .WithMany(pa => pa.Cards)
            .HasForeignKey(c => c.PersonalAccountId);

        base.OnModelCreating(modelBuilder);
    }
}
