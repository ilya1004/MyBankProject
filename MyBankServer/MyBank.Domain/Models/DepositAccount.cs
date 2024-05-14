namespace MyBank.Domain.Models;

public class DepositAccount
{
    public DepositAccount() { }

    public DepositAccount(int id, string name, string number, decimal currentBalance, decimal depositStartBalance, DateTime creationDate, DateTime? closingDate, decimal interestRate, int depositTermInDays, int totalAccrualsNumber, int madeAccrualsNumber, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, bool isActive, int? userId, User? user, int? currencyId, Currency? currency)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        DepositStartBalance = depositStartBalance;
        CreationDate = creationDate;
        ClosingDate = closingDate;
        InterestRate = interestRate;
        DepositTermInDays = depositTermInDays;
        TotalAccrualsNumber = totalAccrualsNumber;
        MadeAccrualsNumber = madeAccrualsNumber;
        IsRevocable = isRevocable;
        HasCapitalisation = hasCapitalisation;
        HasInterestWithdrawalOption = hasInterestWithdrawalOption;
        IsActive = isActive;
        UserId = userId;
        User = user;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public decimal DepositStartBalance { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosingDate { get; set; } = null;
    public decimal InterestRate { get; set; }
    public int DepositTermInDays { get; set; }
    public int TotalAccrualsNumber { get; set; }
    public int MadeAccrualsNumber { get; set; }
    public bool IsRevocable { get; set; }
    public bool HasCapitalisation { get; set; }
    public bool HasInterestWithdrawalOption { get; set; }
    public bool IsActive { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; } = null;
    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; } = null;
    public List<DepositAccrual> Accruals { get; set; } = [];
}
