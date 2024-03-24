namespace MyBank.Application.Interfaces;

public interface IMessagesService
{
    Task<ServiceResponse<int>> Add(Message message);
    Task<ServiceResponse<List<Message>>> GetAllByAdmin(int adminId);
    Task<ServiceResponse<List<Message>>> GetAllByModerator(int moderatorId);
    Task<ServiceResponse<List<Message>>> GetAllByUser(int userId);
    Task<ServiceResponse<Message>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateIsRead(int id, bool isRead);
}