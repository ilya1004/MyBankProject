
namespace MyBank.DataAccess.Entities;

public class TransactionEntity
{
    public TransactionEntity(int id, decimal paymentAmount, DateTime datetime, string status, string information, string accountNumber, int userId, UserEntity? user)
    {
        Id = id;
        PaymentAmount = paymentAmount;
        Datetime = datetime;
        Status = status;
        Information = information;
        AccountNumber = accountNumber;
        UserId = userId;
        User = user;
    }

    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;
    public string Information { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public UserEntity? User { get; set; } = null;
}