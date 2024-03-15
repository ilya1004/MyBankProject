using System.ComponentModel.DataAnnotations;

namespace MyBank.Core.DataTransferObjects.ModeratorDto;

public record RegisterModeratorDto
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public string Nickname { get; set; } = string.Empty;
}
