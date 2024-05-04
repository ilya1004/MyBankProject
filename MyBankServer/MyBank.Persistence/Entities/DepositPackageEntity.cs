using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain.Models;

public class DepositPackageEntity
{
    public DepositPackageEntity() { }
    public DepositPackageEntity(int id, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, bool isActive, int? currencyId, CurrencyEntity? currency)
    {
        Id = id;
        Name = name;
        DepositStartBalance = depositStartBalance;
        InterestRate = interestRate;
        DepositTermInDays = depositTermInDays;
        IsRevocable = isRevocable;
        HasCapitalisation = hasCapitalisation;
        HasInterestWithdrawalOption = hasInterestWithdrawalOption;
        IsActive = isActive;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal DepositStartBalance { get; set; }
    public decimal InterestRate { get; set; }
    public int DepositTermInDays { get; set; }
    public bool IsRevocable { get; set; }
    public bool HasCapitalisation { get; set; }
    public bool HasInterestWithdrawalOption { get; set; }
    public bool IsActive { get; set; }
    public int? CurrencyId { get; set; } = null;
    public CurrencyEntity? Currency { get; set; } = null;
}
