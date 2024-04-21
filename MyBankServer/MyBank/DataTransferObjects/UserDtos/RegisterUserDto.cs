using System.ComponentModel.DataAnnotations;

namespace MyBank.API.DataTransferObjects.UserDtos;

public record RegisterUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PassportSeries { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime BirthdayDate { get; set; }
    public string Citizenship { get; set; } = string.Empty;
}
