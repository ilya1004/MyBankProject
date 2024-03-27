namespace MyBank.API.DataTransferObjects.MessageDtos;

public record MessageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int RecepientId { get; set; }
    public string RecepientRole { get; set; } = string.Empty;
    public DateTime CreationDatetime { get; set; }
    public bool IsRead { get; set; } = false;
    public int? SenderAdminId { get; set; } = null;
    public int? SenderModeratorId { get; set; } = null;
    public int? SenderUserId { get; set; } = null;
}
