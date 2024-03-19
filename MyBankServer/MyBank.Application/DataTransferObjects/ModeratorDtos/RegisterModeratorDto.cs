using System.ComponentModel.DataAnnotations;

namespace MyBank.Domain.DataTransferObjects.ModeratorDtos;

public record RegisterModeratorDto
{
    [Required] public string Login { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public string Nickname { get; set; } = string.Empty;
}
