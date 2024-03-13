using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public MessagesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Message message, int adminId, int moderatorId, int userId)
    {
        var messageEntity = new MessageEntity
        {
            Id = 0,
            Title = message.Title,
            Text = message.Text,
            RecepientId = message.RecepientId,
            RecepientRole = message.RecepientRole,
            IsRead = message.IsRead
        };

        if (adminId != -1)
        {
            var adminEntity = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Id == adminId);

            messageEntity.SenderAdminId = adminId;
            messageEntity.SenderAdmin = adminEntity;
        }
        else if (moderatorId != -1)
        {
            var moderatorEntity = await _dbContext.Moderators
                .FirstOrDefaultAsync(m => m.Id == moderatorId);

            messageEntity.SenderModeratorId = moderatorId;
            messageEntity.SenderModerator = moderatorEntity;
        }
        else if (userId != -1)
        {
            var userEntity = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            messageEntity.SenderUserId = userId;
            messageEntity.SenderUser = userEntity;
        }

        var item = await _dbContext.Messages.AddAsync(messageEntity);
        var number = await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Message> GetById(int id)
    {
        var messageEntity = await _dbContext.Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        return _mapper.Map<Message>(messageEntity);
    }

    public async Task<List<Message>> GetAllByUser(int userId)
    {
        var messageEntitiesList = await _dbContext.Messages
            .AsNoTracking()
            .Where(m => m.SenderUserId == userId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByModerator(int moderatorId)
    {
        var messageEntitiesList = await _dbContext.Messages
            .AsNoTracking()
            .Where(m => m.SenderModeratorId == moderatorId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByAdmin(int adminId)
    {
        var messageEntitiesList = await _dbContext.Messages
            .AsNoTracking()
            .Where(m => m.SenderAdminId == adminId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<bool> UpdateIsRead(int id, bool isRead)
    {
        var number = await _dbContext.Messages
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.IsRead, isRead));

        return (number == 0) ? false : true;
    }
}
