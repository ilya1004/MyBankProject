namespace MyBank.Persistence.Interfaces;

public interface IMessagesRepository
{
    Task<int> Add(Message message);
    Task<Message> GetById(int id);
    Task<List<Message>> GetAllByUser(int userId, bool? isRead);
    Task<List<Message>> GetAllByModerator(int moderatorId, bool? isRead);
    Task<List<Message>> GetAllByAdmin(int adminId, bool? isRead);
    Task<bool> UpdateIsRead(int id, bool isRead);
}
