using MyBank.Persistence.Entities;

namespace MyBank.API.DataTransferObjects.CreditAccountDtos;

public record CreditAccountDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public decimal CreditStartBalance { get; set; }
    public decimal CreditGrantedAmount { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosingDate { get; set; } = null;
    public bool IsActive { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public int MadePaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public int? UserId { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public int? ModeratorApprovedId { get; set; } = null;

}
