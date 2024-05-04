namespace MyBank.API.DataTransferObjects.DepositAccountDtos;

public record DepositAccountDto
{
    public string Name { get; set; } = string.Empty;
    public decimal DepositStartBalance { get; set; }
    public decimal InterestRate { get; set; }
    public int DepositTermInDays { get; set; }
    public bool IsRevocable { get; set; }
    public bool HasCapitalisation { get; set; }
    public bool HasInterestWithdrawalOption { get; set; }
    public int CurrencyId { get; set; }
}
