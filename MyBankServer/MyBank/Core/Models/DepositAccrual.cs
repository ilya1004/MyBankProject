namespace MyBank.Core.Models;

public class DepositAccrual
{
    public DepositAccrual(int id, decimal accrualAmount, DateTime datetime, string status, int depositAccountId, DepositAccount? depositAccount)
    {
        Id = id;
        AccrualAmount = accrualAmount;
        Datetime = datetime;
        Status = status;
        DepositAccountId = depositAccountId;
        DepositAccount = depositAccount;
    }

    public int Id { get; set; }
    public decimal AccrualAmount { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int DepositAccountId { get; set; }
    public DepositAccount? DepositAccount { get; set; }
}