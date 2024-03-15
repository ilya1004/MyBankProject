using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IMessagesService
    {
        Task<int> Add(Message message, int adminId, int moderatorId, int userId);
        Task<List<Message>> GetAllByAdmin(int adminId);
        Task<List<Message>> GetAllByModerator(int moderatorId);
        Task<List<Message>> GetAllByUser(int userId);
        Task<Message> GetById(int id);
        Task<bool> UpdateIsRead(int id, bool isRead);
    }
}