using System.Data;

namespace MyBank.Application.Interfaces;

public interface IMessagesService
{
    Task<ServiceResponse<int>> Add(int senderId, string senderRole, string title, string text, int recepientId, string recepientNickname, string recepientRole);
    Task<ServiceResponse<List<Message>>> GetAllByAdmin(int adminId, bool? isRead);
    Task<ServiceResponse<List<Message>>> GetAllByModerator(int moderatorId, bool? isRead);
    Task<ServiceResponse<List<Message>>> GetAllByUser(int userId, bool? isRead);
    Task<ServiceResponse<Message>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateIsRead(int id, bool isRead);
}