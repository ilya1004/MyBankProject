using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class DepositAccrualEntity
{
    public DepositAccrualEntity() { }

    public DepositAccrualEntity(int id, decimal accrualAmount, int accrualNumber, DateTime datetime, bool status, int? depositAccountId, DepositAccountEntity? depositAccount)
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
    [Column(TypeName = "money")]
    public decimal AccrualAmount { get; set; }
    public int AccrualNumber { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; } = true;
    public int? DepositAccountId { get; set; } = null;
    public DepositAccountEntity? DepositAccount { get; set; }
}