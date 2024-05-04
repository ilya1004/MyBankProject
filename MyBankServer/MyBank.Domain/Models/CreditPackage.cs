namespace MyBank.Domain.Models;

public class CreditPackage
{
    public CreditPackage() { }

    public CreditPackage(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate, string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, bool isActive, int? currencyId, Currency? currency)
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
    public decimal CreditStartBalance { get; set; }
    public decimal CreditGrantedAmount { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;  // "annuity", "differential"
    public int CreditTermInDays { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public bool IsActive { get; set; } = true;
    public int? CurrencyId { get; set; } = null;
    public Currency? Currency { get; set; } = null;
    public List<CreditRequest> CreditRequests { get; set; } = [];
}
