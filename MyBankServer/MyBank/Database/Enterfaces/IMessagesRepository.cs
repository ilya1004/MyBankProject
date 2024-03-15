using MyBank.Core.Models;

namespace MyBank.Database.Enterfaces;

public interface IMessagesRepository
{
    Task<int> Add(Message message, int adminId, int moderatorId, int userId);

    Task<Message> GetById(int id);

    Task<List<Message>> GetAllByUser(int userId);

    Task<List<Message>> GetAllByModerator(int moderatorId);

    Task<List<Message>> GetAllByAdmin(int adminId);

    Task<bool> UpdateIsRead(int id, bool isRead);
}