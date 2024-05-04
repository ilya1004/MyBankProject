namespace MyBank.API.DataTransferObjects.ModeratorDtos;

public record UpdateModeratorDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
}
