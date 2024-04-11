namespace MyBank.API.DataTransferObjects.UserDtos;

public record UpdateUserEmailDto
{
    public string OldEmail { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty;
}
