namespace MyBank.API.DataTransferObjects.CardDto;

public record CardDto
{
    public string Name { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public int DurationInYears {  get; set; }
    public int? CardPackageId { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
}
