namespace MyBank.Application.Services;


public class MessagesService : IMessagesService
{
    private readonly IMessagesRepository _messagesRepository;
    public MessagesService(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }

    public async Task<ServiceResponse<int>> Add(Message message)
    {
        var id = await _messagesRepository.Add(message);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<Message>> GetById(int id)
    {
        var message = await _messagesRepository.GetById(id);

        if (message == null)
        {
            return new ServiceResponse<Message> { Status = false, Message = $"Message with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<Message> { Status = true, Message = "Success", Data = message };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByUser(int userId)
    {
        var list = await _messagesRepository.GetAllByUser(userId);

        return new ServiceResponse<List<Message>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByModerator(int moderatorId)
    {
        var list = await _messagesRepository.GetAllByModerator(moderatorId);

        return new ServiceResponse<List<Message>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByAdmin(int adminId)
    {
        var list = await _messagesRepository.GetAllByAdmin(adminId);

        return new ServiceResponse<List<Message>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateIsRead(int id, bool isRead)
    {
        var status = await _messagesRepository.UpdateIsRead(id, isRead);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe message with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
