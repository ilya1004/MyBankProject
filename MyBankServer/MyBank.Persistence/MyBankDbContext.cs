using Microsoft.EntityFrameworkCore;
using MyBank.Persistence.Configurations;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence;

public class MyBankDbContext : DbContext
{
    public MyBankDbContext(DbContextOptions<MyBankDbContext> options) :
        base(options)
    { }
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<CardPackageEntity> CardPackages { get; set; }
    public DbSet<CreditAccountEntity> CreditAccounts { get; set; }
    public DbSet<CreditPaymentEntity> CreditPayments { get; set; }
    public DbSet<CreditRequestEntity> CreditRequests { get; set; }
    public DbSet<CurrencyEntity> Currencies { get; set; }
    public DbSet<DepositAccountEntity> DepositAccounts { get; set; }
    public DbSet<DepositAccrualEntity> DepositAccruals { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<ModeratorEntity> Moderators { get; set; }
    public DbSet<PersonalAccountEntity> PersonalAccounts { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CardPackageConfiguration());
        modelBuilder.ApplyConfiguration(new CreditAccountConfiguration());
        modelBuilder.ApplyConfiguration(new CreditPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new CreditRequestConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new DepositAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DepositAccrualConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new ModeratorConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalAccountConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
