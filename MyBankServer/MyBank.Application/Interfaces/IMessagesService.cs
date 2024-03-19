using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface IMessagesService
{
    Task<ServiceResponse<int>> Add(Message message, int adminId, int moderatorId, int userId);
    Task<ServiceResponse<List<Message>>> GetAllByAdmin(int adminId);
    Task<ServiceResponse<List<Message>>> GetAllByModerator(int moderatorId);
    Task<ServiceResponse<List<Message>>> GetAllByUser(int userId);
    Task<ServiceResponse<Message>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateIsRead(int id, bool isRead);
}