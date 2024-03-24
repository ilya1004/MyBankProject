namespace MyBank.Domain.Models;

public class CreditRequest
{
    public CreditRequest(int id, decimal startBalance, decimal interestRate, string interestCalculationType, int creditTermInDays, int totalPaymentsNumber, bool hasPrepaymentOption, bool isApproved, int moderatorId, Moderator? moderator, int userId, User? user)
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
    }

    public int Id { get; set; }
    public decimal StartBalance { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public bool IsApproved { get; set; }
    public int? ModeratorId { get; set; }
    public Moderator? Moderator { get; set; } = null;
    public int? UserId { get; set; }
    public User? User { get; set; } = null;
}