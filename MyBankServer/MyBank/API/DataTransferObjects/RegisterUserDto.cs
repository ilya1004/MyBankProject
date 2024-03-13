using System.ComponentModel.DataAnnotations;

namespace MyBank.API.DataTransferObjects;

public record RegisterUserDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Required] public string Nickname { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Surname { get; set; } = string.Empty;
    [Required] public string PassportSeries { get; set; } = string.Empty;
    [Required] public string PassportNumber { get; set; } = string.Empty;

}
