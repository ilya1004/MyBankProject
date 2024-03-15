using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class MessagesService : IMessagesService
{
    private readonly IMessagesRepository _messagesRepository;
    public MessagesService(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }

    public async Task<int> Add(Message message, int adminId, int moderatorId, int userId)
    {
        return await _messagesRepository.Add(message, adminId, moderatorId, userId);
    }

    public async Task<Message> GetById(int id)
    {
        return await _messagesRepository.GetById(id);
    }

    public async Task<List<Message>> GetAllByUser(int userId)
    {
        return await _messagesRepository.GetAllByUser(userId);
    }

    public async Task<List<Message>> GetAllByModerator(int moderatorId)
    {
        return await _messagesRepository.GetAllByModerator(moderatorId);
    }

    public async Task<List<Message>> GetAllByAdmin(int adminId)
    {
        return await _messagesRepository.GetAllByAdmin(adminId);
    }

    public async Task<bool> UpdateIsRead(int id, bool isRead)
    {
        return await _messagesRepository.UpdateIsRead(id, isRead);
    }
}
