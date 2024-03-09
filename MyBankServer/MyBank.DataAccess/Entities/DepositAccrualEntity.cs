
namespace MyBank.DataAccess.Entities;

public class DepositAccrualEntity
{
    public DepositAccrualEntity(int id, decimal accrualAmount, DateTime datetime, string status)
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
    public int DepositAccountId { get; set; }
    public DepositAccountEntity? DepositAccount { get; set; }
}
