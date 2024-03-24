namespace MyBank.Domain.Models;

public class CreditAccount
{
    public CreditAccount() { }
    public CreditAccount(int id, string name, string number, decimal currentBalance, decimal creditStartBalance, DateTime creationDate, DateTime closingDate, bool isActive, decimal interestRate, string interestCalculationType, int creditTermInDays, int totalPaymentsNumber, int madePaymentsNumber, bool hasPrepaymentOption, int? userId, User? userOwner, int? currencyId, Currency? currency, int? moderatorApprovedId, Moderator? moderatorApproved)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        CreditStartBalance = creditStartBalance;
        CreationDate = creationDate;
        ClosingDate = closingDate;
        IsActive = isActive;
        InterestRate = interestRate;
        InterestCalculationType = interestCalculationType;
        CreditTermInDays = creditTermInDays;
        TotalPaymentsNumber = totalPaymentsNumber;
        MadePaymentsNumber = madePaymentsNumber;
        HasPrepaymentOption = hasPrepaymentOption;
        UserId = userId;
        UserOwner = userOwner;
        CurrencyId = currencyId;
        Currency = currency;
        ModeratorApprovedId = moderatorApprovedId;
        ModeratorApproved = moderatorApproved;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public decimal CreditStartBalance { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public int MadePaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public int? UserId { get; set; } = null;
    public User? UserOwner { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public Currency? Currency { get; set; } = null;
    public int? ModeratorApprovedId { get; set; } = null;
    public Moderator? ModeratorApproved { get; set; } = null;
    public List<CreditPayment> Payments { get; set; } = [];
    public List<Transaction> Transactions { get; set; } = [];
}
