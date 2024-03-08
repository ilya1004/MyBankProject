
namespace MyBank.DataAccess.Entities;

public class MessageEntity
{
    public MessageEntity(int id, string title, string text, int? senderAdminId, AdminEntity? senderAdmin, int? senderModeratorId, ModeratorEntity? senderModerator, int? senderUserId, UserEntity? senderUser, DateTime creationDatetime, bool isRead)
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
    public AdminEntity? SenderAdmin { get; set; } = null;
    public int? SenderModeratorId { get; set; } = null;
    public ModeratorEntity? SenderModerator { get; set; } = null;
    public int? SenderUserId { get; set; } = null;
    public UserEntity? SenderUser { get; set; } = null;
    public DateTime CreationDatetime { get; set; }
    public bool IsRead { get; set; } = false;
}