namespace MyBank.Core.Models;

public class DepositAccrual
{
    public DepositAccrual(int id, decimal accrualAmount, DateTime datetime, string status)
    {
        Id = id;
        AccrualAmount = accrualAmount;
        Datetime = datetime;
        Status = status;
    }

    public int Id { get; set; }
    public decimal AccrualAmount { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? DepositAccountId { get; set; } = null;
    public DepositAccount? DepositAccount { get; set; } = null;
}