using System.ComponentModel.DataAnnotations;

namespace MyBank.API.DataTransferObjects.AdminDtos;

public record LoginAdminDto
{
    [Required]
    public string Login { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}
