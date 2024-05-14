namespace MyBank.API.DataTransferObjects.CreditRequestDtos;

public record CreditRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int CreditPackageId { get; set; }
    public int UserId { get; set; }
}
