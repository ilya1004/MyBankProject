namespace MyBank.Persistence.Entities;

public class MessageEntity
{
    public MessageEntity() { }

    public MessageEntity(
        int id,
        string title,
        string text,
        int recepientId,
        string recepientRole,
        DateTime creationDatetime,
        bool isRead
    )
    {
        Id = id;
        Title = title;
        Text = text;
        RecepientId = recepientId;
        RecepientRole = recepientRole;
        CreationDatetime = creationDatetime;
        IsRead = isRead;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int RecepientId { get; set; }
    public string RecepientRole { get; set; } = string.Empty;
    public DateTime CreationDatetime { get; set; }
    public bool IsRead { get; set; } = false;
    public int? SenderAdminId { get; set; } = null;
    public AdminEntity? SenderAdmin { get; set; } = null;
    public int? SenderModeratorId { get; set; } = null;
    public ModeratorEntity? SenderModerator { get; set; } = null;
    public int? SenderUserId { get; set; } = null;
    public UserEntity? SenderUser { get; set; } = null;
}
