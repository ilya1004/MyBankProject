namespace MyBank.API.DataTransferObjects.CreditRequestDtos;

public record CreditRequestDto
{
    public decimal StartBalance { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;
    public int CreditTermInDays { get; set; }
    public int TotalPaymentsNumber { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public int? UserId { get; set; }
    public int? CurrencyId { get; set; } = null;
}
