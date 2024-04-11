namespace MyBank.Domain.Models;

public class Message
{
    public Message() { }

    public Message(
        int id,
        string title,
        string text,
        int recepientId,
        string recepientRole,
        DateTime creationDatetime,
        bool isRead,
        int? senderAdminId,
        Admin? senderAdmin,
        int? senderModeratorId,
        Moderator? senderModerator,
        int? senderUserId,
        User? senderUser
    )
    {
        Id = id;
        Title = title;
        Text = text;
        RecepientId = recepientId;
        RecepientRole = recepientRole;
        CreationDatetime = creationDatetime;
        IsRead = isRead;
        SenderAdminId = senderAdminId;
        SenderAdmin = senderAdmin;
        SenderModeratorId = senderModeratorId;
        SenderModerator = senderModerator;
        SenderUserId = senderUserId;
        SenderUser = senderUser;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int RecepientId { get; set; }
    public string RecepientRole { get; set; } = string.Empty;
    public DateTime CreationDatetime { get; set; }
    public bool IsRead { get; set; } = false;
    public int? SenderAdminId { get; set; } = null;
    public Admin? SenderAdmin { get; set; } = null;
    public int? SenderModeratorId { get; set; } = null;
    public Moderator? SenderModerator { get; set; } = null;
    public int? SenderUserId { get; set; } = null;
    public User? SenderUser { get; set; } = null;
}
