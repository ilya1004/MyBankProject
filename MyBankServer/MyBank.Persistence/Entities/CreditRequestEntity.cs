using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CreditRequestEntity
{
    public CreditRequestEntity() { }

    public CreditRequestEntity(int id, decimal startBalance, decimal interestRate, string interestCalculationType, int creditTermInDays, int totalPaymentsNumber, bool hasPrepaymentOption, bool? isApproved, int? moderatorId, ModeratorEntity? moderator, int? userId, UserEntity? user, int? currencyId, CurrencyEntity? currency)
    {
        Id = id;
        StartBalance = startBalance;
        InterestRate = interestRate;
        InterestCalculationType = interestCalculationType;
        CreditTermInDays = creditTermInDays;
        TotalPaymentsNumber = totalPaymentsNumber;
        HasPrepaymentOption = hasPrepaymentOption;
        IsApproved = isApproved;
        ModeratorId = moderatorId;
        Moderator = moderator;
        UserId = userId;
        User = user;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    [Column(TypeName = "money")]
    public decimal StartBalance { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public bool? IsApproved { get; set; } = null;
    public int? ModeratorId { get; set; } = null;
    public ModeratorEntity? Moderator { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public CurrencyEntity? Currency { get; set; } = null;
}
