namespace MyBank.Core.Models;

public class DepositAccrual
{
    public DepositAccrual(int id, decimal accrualAmount, DateTime datetime, string status, int depositAccountId, DepositAccount? depositAccount, int userId, User? user)
    {
        Id = id;
        AccrualAmount = accrualAmount;
        Datetime = datetime;
        Status = status;
        DepositAccountId = depositAccountId;
        DepositAccount = depositAccount;
        UserId = userId;
        User = user;
    }

    public int Id { get; set; }
    public decimal AccrualAmount { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int DepositAccountId { get; set; }
    public DepositAccount? DepositAccount { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}