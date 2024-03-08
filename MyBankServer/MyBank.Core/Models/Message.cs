
namespace MyBank.Core.Models;

public class Message
{
    public Message(int id, string title, string text, int? senderAdminId, Admin? senderAdmin, int? senderModeratorId, Moderator? senderModerator, int? senderUserId, User? senderUser, DateTime creationDatetime, bool isRead)
    {
        Id = id;
        Title = title;
        Text = text;
        SenderAdminId = senderAdminId;
        SenderAdmin = senderAdmin;
        SenderModeratorId = senderModeratorId;
        SenderModerator = senderModerator;
        SenderUserId = senderUserId;
        SenderUser = senderUser;
        CreationDatetime = creationDatetime;
        IsRead = isRead;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int? SenderAdminId { get; set; } = null;
    public Admin? SenderAdmin { get; set; } = null;
    public int? SenderModeratorId { get; set; } = null;
    public Moderator? SenderModerator { get; set; } = null;
    public int? SenderUserId { get; set; } = null;
    public User? SenderUser { get; set; } = null;
    public DateTime CreationDatetime { get; set; }
    public bool IsRead { get; set; } = false;
}
