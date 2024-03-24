namespace MyBank.API.DataTransferObjects.CardDto;

public record UpdatePincodeDto
{
    public int Id { get; set; }
    public string Pincode { get; set; } = string.Empty;
}
