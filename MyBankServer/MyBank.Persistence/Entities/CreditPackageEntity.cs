using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Domain.Models;

public class CreditPackageEntity
{
    public CreditPackageEntity() { }

    public CreditPackageEntity(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate, string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, bool isActive, int? currencyId, CurrencyEntity? currency)
    {
        Id = id;
        Name = name;
        CreditStartBalance = creditStartBalance;
        CreditGrantedAmount = creditGrantedAmount;
        InterestRate = interestRate;
        InterestCalculationType = interestCalculationType;
        CreditTermInDays = creditTermInDays;
        HasPrepaymentOption = hasPrepaymentOption;
        IsActive = isActive;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal CreditStartBalance { get; set; }
    [Column(TypeName = "money")]
    public decimal CreditGrantedAmount { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;  // "annuity", "differential"
    public int CreditTermInDays { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public bool IsActive { get; set; } = true;
    public int? CurrencyId { get; set; } = null;
    public CurrencyEntity? Currency { get; set; } = null;
    public List<CreditRequestEntity> CreditRequests { get; set; } = [];
}
