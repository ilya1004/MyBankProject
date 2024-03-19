using System.ComponentModel.DataAnnotations;

namespace MyBank.Core.DataTransferObjects.ModeratorDtos;

public record LoginModeratorDto
{
    [Required] public string Login { get; init; } = string.Empty;
    [Required] public string Password { get; init; } = string.Empty;
}
