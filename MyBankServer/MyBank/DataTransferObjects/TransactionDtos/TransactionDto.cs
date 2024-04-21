namespace MyBank.API.DataTransferObjects.TransactionDtos;

public record TransactionDto
{
    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime Datetime { get; set; }
    public bool Status { get; set; }
    public string Information { get; set; } = string.Empty;
    public string? AccountSenderNumber { get; set; } = null;
    public string? UserSenderNickname { get; set; } = null;
    public string? AccountRecipientNumber { get; set; } = null;
    public string? UserRecipientNickname { get; set; } = null;
}
