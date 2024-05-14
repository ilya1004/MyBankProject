namespace MyBank.Domain.Models;

public class DepositAccrual
{
    public DepositAccrual() { }

    public DepositAccrual(int id, decimal accrualAmount, int accrualNumber, DateTime datetime, bool status, int? depositAccountId, DepositAccount? depositAccount)
    {
        Id = id;
        AccrualAmount = accrualAmount;
        AccrualNumber = accrualNumber;
        Datetime = datetime;
        Status = status;
        DepositAccountId = depositAccountId;
        DepositAccount = depositAccount;
    }

    public int Id { get; set; }
    public decimal AccrualAmount { get; set; }
    public int AccrualNumber { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; } = true;
    public int? DepositAccountId { get; set; } = null;
    public DepositAccount? DepositAccount { get; set; } = null;
}
