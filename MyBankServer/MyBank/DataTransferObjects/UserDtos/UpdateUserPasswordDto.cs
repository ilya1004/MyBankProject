namespace MyBank.API.DataTransferObjects.UserDtos;

public record UpdateUserPasswordDto
{
    public string OldEmail { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
