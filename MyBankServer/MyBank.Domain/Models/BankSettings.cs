namespace MyBank.Domain.Models;

public class BankSettings
{
    public int Id { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal DepositInterestRate { get; set; }
    public DateTime LastUpdate { get; set; }
}
