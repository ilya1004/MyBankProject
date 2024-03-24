namespace MyBank.API.DataTransferObjects.CardDto;

public record CardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CvvCode { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int? CardPackageId { get; set; } = null;
    public int? UserId { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
}
