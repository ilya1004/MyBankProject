namespace MyBank.API.DataTransferObjects.PersonalAccountDtos;

public record TransferDto
{
    public int PersonalAccountId { get; set; }
    public string AccountSenderNumber { get; set; } = string.Empty;
    public string UserSenderNickname { get; set; } = string.Empty;
    public string? AccountRecipientNumber { get; set; }
    public string? CardRecipientNumber { get; set; }
    public string? UserRecipientNickname { get; set; }
    public decimal Amount { get; set; }
}
