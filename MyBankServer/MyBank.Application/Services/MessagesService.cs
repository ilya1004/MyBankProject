namespace MyBank.Application.Services;

public class MessagesService : IMessagesService
{
    private readonly IMessagesRepository _messagesRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IModeratorsRepository _moderatorsRepository;

    public MessagesService(IMessagesRepository messagesRepository, IAdminRepository adminRepository, IModeratorsRepository moderatorsRepository)
    {
        _messagesRepository = messagesRepository;
        _adminRepository = adminRepository;
        _moderatorsRepository = moderatorsRepository;
    }

    public async Task<ServiceResponse<int>> Add(int senderId, string senderRole, string title, string text, int recepientId, string recepientNickname, string recepientRole)
    {
        var message = new Message
        {
            Id = 0,
            Title = title,
            Text = text,
            RecepientId = recepientId,
            RecepientNickname = recepientNickname,
            RecepientRole = recepientRole,
            CreationDatetime = DateTime.UtcNow,
            IsRead = false,
            IsActive = true,
            SenderAdmin = null,
            SenderAdminId = null,
            SenderModerator = null,
            SenderModeratorId = null,
            SenderUser = null,
            SenderUserId = null,
        };

        if (senderRole == "admin")
        {
            message.SenderAdminId = senderId;
        }
        else if (senderRole == "moderator")
        {
            message.SenderModeratorId = senderId;
        }
        else if (senderRole == "user")
        {
            if (recepientRole == "admin")
            {
                var admin = (await _adminRepository.GetAll())[0];
                message.RecepientId = admin.Id;
                message.RecepientNickname = "";
            }
            else if (recepientRole == "moderator")
            {
                var moderators = await _moderatorsRepository.GetAll(false, true);

                var random = new Random();
                var randomModerator = moderators[random.Next(moderators.Count)];
                message.RecepientId = randomModerator.Id;
                message.RecepientNickname = randomModerator.Nickname;
            }
            message.SenderUserId = senderId;
        }

        var id = await _messagesRepository.Add(message);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<Message>> GetById(int id)
    {
        var message = await _messagesRepository.GetById(id);

        if (message == null)
        {
            return new ServiceResponse<Message>
            {
                Status = false,
                Message = $"Message with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Message>
        {
            Status = true,
            Message = "Success",
            Data = message
        };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByUser(int userId, bool? isRead)
    {
        var list = await _messagesRepository.GetAllByUser(userId, isRead);

        return new ServiceResponse<List<Message>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByModerator(int moderatorId, bool? isRead)
    {
        var list = await _messagesRepository.GetAllByModerator(moderatorId, isRead);

        return new ServiceResponse<List<Message>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Message>>> GetAllByAdmin(int adminId, bool? isRead)
    {
        var list = await _messagesRepository.GetAllByAdmin(adminId, isRead);

        return new ServiceResponse<List<Message>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateIsRead(int id, bool isRead)
    {
        var status = await _messagesRepository.UpdateIsRead(id, isRead);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe message with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
