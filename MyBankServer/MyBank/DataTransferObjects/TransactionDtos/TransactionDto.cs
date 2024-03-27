namespace MyBank.API.DataTransferObjects.TransactionDtos;

public record TransactionDto
{
    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; }
    public string Information { get; set; } = string.Empty;
    public string? AccountReceiverNumber { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
}
