
namespace MyBank.DataAccess.Entities;

public class CreditPaymentEntity
{
    public CreditPaymentEntity(int id, decimal paymentAmount, int paymentNumber, DateTime datetime, string status, int creditAccountId, CreditAccountEntity? creditAccount, int userId, UserEntity? user)
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
    public int CreditAccountId { get; set; }
    public CreditAccountEntity? CreditAccount { get; set; }
    public int UserId { get; set; }
    public UserEntity? User { get; set; }
}
