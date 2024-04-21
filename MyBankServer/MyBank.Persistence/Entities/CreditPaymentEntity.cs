using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CreditPaymentEntity
{
    public CreditPaymentEntity() { }

    public CreditPaymentEntity(
        int id,
        decimal paymentAmount,
        int paymentNumber,
        DateTime datetime,
        string status
    )
    {
        Id = id;
        PaymentAmount = paymentAmount;
        PaymentNumber = paymentNumber;
        Datetime = datetime;
        Status = status;
    }

    public int Id { get; set; }
    [Column(TypeName = "money")]
    public decimal PaymentAmount { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? CreditAccountId { get; set; } = null;
    public CreditAccountEntity? CreditAccount { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
}
