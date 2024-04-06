namespace MyBank.Domain.Models;

public class CreditPayment
{
    public CreditPayment() { }
    public CreditPayment(int id, decimal paymentAmount, int paymentNumber, DateTime datetime, string status, int creditAccountId, CreditAccount? creditAccount, int userId, User? user)
    {
        Id = id;
        PaymentAmount = paymentAmount;
        PaymentNumber = paymentNumber;
        Datetime = datetime;
        Status = status;
        CreditAccountId = creditAccountId;
        CreditAccount = creditAccount;
        UserId = userId;
        User = user;
    }

    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? CreditAccountId { get; set; }
    public CreditAccount? CreditAccount { get; set; } = null;
    public int? UserId { get; set; }
    public User? User { get; set; } = null;
}