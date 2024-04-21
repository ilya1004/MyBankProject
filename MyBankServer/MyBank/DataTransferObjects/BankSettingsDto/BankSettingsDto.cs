namespace MyBank.API.DataTransferObjects.BankSettingsDto;

public class BankSettingsDto
{
    public int Id { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal DepositInterestRate { get; set; }
}
