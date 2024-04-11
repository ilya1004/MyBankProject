using System.ComponentModel.DataAnnotations;

namespace MyBank.API.DataTransferObjects.UserDtos;

public record LoginUserDto
{
    [Required]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}
