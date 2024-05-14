using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CreditPaymentEntity
{
    public CreditPaymentEntity() { }

    public CreditPaymentEntity(int id, decimal paymentAmount, int paymentNumber, DateTime datetime, bool status, int? creditAccountId, CreditAccountEntity? creditAccount, int? userId, UserEntity? user)
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
    [Column(TypeName = "money")]
    public decimal PaymentAmount { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; } = true;
    public int? CreditAccountId { get; set; } = null;
    public CreditAccountEntity? CreditAccount { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
}
