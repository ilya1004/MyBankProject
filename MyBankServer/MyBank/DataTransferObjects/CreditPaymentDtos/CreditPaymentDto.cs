namespace MyBank.API.DataTransferObjects.CreditPaymentDtos;

public record CreditPaymentDto
{
    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime Datetime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? CreditAccountId { get; set; }
    public int? UserId { get; set; }
}
