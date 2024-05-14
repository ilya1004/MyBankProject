namespace MyBank.API.DataTransferObjects.MessageDtos;

public record MessageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int RecepientId { get; set; }
    public string RecepientNickname { get; set; } = string.Empty;
    public string RecepientRole { get; set; } = string.Empty;
}
