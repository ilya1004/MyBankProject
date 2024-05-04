namespace MyBank.API.DataTransferObjects.CreditPaymentDtos;

public record CreditPaymentDto
{
    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public int PaymentNumber { get; set; }
    public int CreditAccountId { get; set; }
    public string CreditAccountNumber { get; set; } = string.Empty;
    public int PersonalAccountId { get; set; }
    public string PersonalAccountNumber { get; set; } = string.Empty;
    public string UserNickname {  get; set; } = string.Empty;
}
