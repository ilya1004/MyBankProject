namespace MyBank.API.DataTransferObjects.TransactionDtos;

public record TransactionDto
{
    public decimal PaymentAmount { get; set; }
    public int CurrencyId { get; set; }
    public string Information { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public string AccountSenderNumber { get; set; } = string.Empty;
    public string UserSenderNickname { get; set; } = string.Empty;
    public string? CardRecipientNumber { get; set; } = null;
    public string? AccountRecipientNumber { get; set; } = null;
    public string? UserRecipientNickname { get; set; } = null;
}
