namespace MyBank.Domain.Models;

public class Currency
{
    public Currency() { }

    public Currency(int id, string code, string name, int scale, DateTime lastRateUpdate, decimal officialRate, decimal creditInterestRate, decimal depositInterestRate, bool isActive)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        LastRateUpdate = lastRateUpdate;
        OfficialRate = officialRate;
        CreditInterestRate = creditInterestRate;
        DepositInterestRate = depositInterestRate;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Scale { get; set; }
    public DateTime LastRateUpdate { get; set; }
    public decimal OfficialRate { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal DepositInterestRate { get; set; }
    public bool IsActive { get; set; } = true;
    public List<PersonalAccount> PersonalAccounts { get; set; } = [];
    public List<CreditAccount> CreditAccounts { get; set; } = [];
    public List<DepositAccount> DepositAccounts { get; set; } = [];
    public List<CreditRequest> CreditRequests { get; set; } = [];
}
