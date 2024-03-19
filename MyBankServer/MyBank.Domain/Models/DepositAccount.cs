namespace MyBank.Domain.Models;

public class DepositAccount
{
    public DepositAccount(int id, string name, string number, decimal currentBalance, decimal depositStartBalance, DateTime creationDate, DateTime closingDate, bool isActive, decimal interestRate, int depositTermInDays, int totalAccrualsNumber, int madeAccrualsNumber, bool isRevocable, string interestPaymentType, bool hasCapitalisation, bool hasInterestWithdrawalOption, int userId, User? userOwner, int currencyId, Currency? currency)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        DepositStartBalance = depositStartBalance;
        CreationDate = creationDate;
        ClosingDate = closingDate;
        IsActive = isActive;
        InterestRate = interestRate;
        DepositTermInDays = depositTermInDays;
        TotalAccrualsNumber = totalAccrualsNumber;
        MadeAccrualsNumber = madeAccrualsNumber;
        IsRevocable = isRevocable;
        InterestPaymentType = interestPaymentType;
        HasCapitalisation = hasCapitalisation;
        HasInterestWithdrawalOption = hasInterestWithdrawalOption;
        UserId = userId;
        UserOwner = userOwner;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public decimal DepositStartBalance { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public decimal InterestRate { get; set; }
    public int DepositTermInDays { get; set; }
    public int TotalAccrualsNumber { get; set; }
    public int MadeAccrualsNumber { get; set; }
    public bool IsRevocable { get; set; }
    public string InterestPaymentType { get; set; } = string.Empty;
    public bool HasCapitalisation { get; set; }
    public bool HasInterestWithdrawalOption { get; set; }
    public int UserId { get; set; }
    public User? UserOwner { get; set; } = null;
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; } = null;
    public List<DepositAccrual> Accruals { get; set; } = [];
}