namespace MyBank.Persistence.Entities;

public class TransactionEntity
{
    public TransactionEntity() { }

    public TransactionEntity(int id, decimal paymentAmount, DateTime datetime, bool status, string information, string? accountSenderNumber, string? userSenderNickname, string? accountRecipientNumber, string? userRecipientNickname)
    {
        Id = id;
        PaymentAmount = paymentAmount;
        Datetime = datetime;
        Status = status;
        Information = information;
        AccountSenderNumber = accountSenderNumber;
        UserSenderNickname = userSenderNickname;
        AccountRecipientNumber = accountRecipientNumber;
        UserRecipientNickname = userRecipientNickname;
    }

    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; }
    public string Information { get; set; } = string.Empty;
    public string? AccountSenderNumber { get; set; } = null;
    public string? UserSenderNickname { get; set; } = null;
    public string? AccountRecipientNumber { get; set; } = null;
    public string? UserRecipientNickname { get; set; } = null;
}