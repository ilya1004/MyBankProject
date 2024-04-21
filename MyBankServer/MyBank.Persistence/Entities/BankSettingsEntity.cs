namespace MyBank.Persistence.Entities;

public class BankSettingsEntity
{
    public int Id { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal DepositInterestRate { get; set; }
    public DateTime LastUpdate { get; set; }
}
