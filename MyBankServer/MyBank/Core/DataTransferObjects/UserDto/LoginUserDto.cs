using System.ComponentModel.DataAnnotations;

namespace MyBank.Core.DataTransferObjects.UserDto;

public record LoginUserDto
{
    [Required] public string Email { get; init; } = string.Empty;
    [Required] public string Password { get; init; } = string.Empty;
}
