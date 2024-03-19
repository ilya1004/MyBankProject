namespace MyBank.Persistence.Entities;

public class TransactionEntity
{
    public TransactionEntity() { }
    public TransactionEntity(int id, decimal paymentAmount, DateTime datetime, string status, string information, string? accountReceiverNumber)
    {
        Id = id;
        PaymentAmount = paymentAmount;
        Datetime = datetime;
        Status = status;
        Information = information;
        AccountReceiverNumber = accountReceiverNumber;
    }

    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;
    public string Information { get; set; } = string.Empty;
    public string? AccountReceiverNumber { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccountEntity? PersonalAccount { get; set; } = null;
}