
namespace MyBank.Domain.Models;

public class Transaction
{
    public Transaction(int id, decimal paymentAmount, DateTime datetime, string status, string information, string? accountReceiverNumber, int? personalAccountId, PersonalAccount? personalAccount, int? creditAccountId, CreditAccount? creditAccount)
    {
        Id = id;
        PaymentAmount = paymentAmount;
        Datetime = datetime;
        Status = status;
        Information = information;
        AccountReceiverNumber = accountReceiverNumber;
        PersonalAccountId = personalAccountId;
        PersonalAccount = personalAccount;
        CreditAccountId = creditAccountId;
        CreditAccount = creditAccount;
    }

    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;
    public string Information { get; set; } = string.Empty;
    public string? AccountReceiverNumber { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccount? PersonalAccount { get; set; } = null;
    public int? CreditAccountId { get; set; } = null;
    public CreditAccount? CreditAccount { get; set; } = null;
}
