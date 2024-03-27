namespace MyBank.Persistence.Interfaces;

public interface IMessagesRepository
{
    Task<int> Add(Message message);

    Task<Message> GetById(int id);

    Task<List<Message>> GetAllByUser(int userId);

    Task<List<Message>> GetAllByModerator(int moderatorId);

    Task<List<Message>> GetAllByAdmin(int adminId);

    Task<bool> UpdateIsRead(int id, bool isRead);
}