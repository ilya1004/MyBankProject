namespace MyBank.API.DataTransferObjects.CreditPackageDtos;

public record CreditPackageDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CreditStartBalance { get; set; }
    public decimal CreditGrantedAmount { get; set; }
    public decimal InterestRate { get; set; }
    public string InterestCalculationType { get; set; } = string.Empty;  // "annuity", "differential"
    public int CreditTermInDays { get; set; }
    public bool HasPrepaymentOption { get; set; }
    public int CurrencyId { get; set; }
}
