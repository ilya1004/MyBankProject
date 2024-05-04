using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CreditAccountEntity
{
    public CreditAccountEntity() { }

    public CreditAccountEntity(int id, string name, string number, decimal currentBalance, decimal creditStartBalance, decimal creditGrantedAmount, DateTime creationDate, DateTime? closingDate, bool isActive, decimal interestRate, string interestCalculationType, int creditTermInDays, int totalPaymentsNumber, int madePaymentsNumber, bool hasPrepaymentOption, int? userId, UserEntity? user, int? currencyId, CurrencyEntity? currency, int? moderatorApprovedId, ModeratorEntity? moderatorApproved)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        CreditStartBalance = creditStartBalance;
        CreditGrantedAmount = creditGrantedAmount;
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
        User = user;
        CurrencyId = currencyId;
        Currency = currency;
        ModeratorApprovedId = moderatorApprovedId;
        ModeratorApproved = moderatorApproved;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal CurrentBalance { get; set; }
    [Column(TypeName = "money")]
    public decimal CreditStartBalance { get; set; }
    [Column(TypeName = "money")]
    public decimal CreditGrantedAmount { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosingDate { get; set; } = null;
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;  // "annuity", "differential"
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public int MadePaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public bool IsActive { get; set; }
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public CurrencyEntity? Currency { get; set; } = null;
    public int? ModeratorApprovedId { get; set; } = null;
    public ModeratorEntity? ModeratorApproved { get; set; } = null;
    public List<CreditPaymentEntity> Payments { get; set; } = [];
}
