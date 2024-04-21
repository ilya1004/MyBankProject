namespace MyBank.API.DataTransferObjects.TransactionDtos;

public record TransactionsByDateDto
{
    public string PersonalAccountNumber { get; set; } = string.Empty;
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; } 
}
