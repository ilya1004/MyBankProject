using MyBank.Domain.Models;

namespace MyBank.Persistence.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessagesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Message message)
    {
        var messageEntity = _mapper.Map<MessageEntity>(message);

        if (message.SenderAdminId != null)
        {
            messageEntity.SenderAdmin = await _dbContext.Admins
                .FirstOrDefaultAsync(a => a.Id == message.SenderAdminId);
        }
        else if (message.SenderModeratorId != null)
        {
            messageEntity.SenderModerator = await _dbContext.Moderators
                .FirstOrDefaultAsync(m => m.Id == message.SenderModeratorId);
        }
        else if (message.SenderUserId != null)
        {
            messageEntity.SenderUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == message.SenderUserId);
        }

        var item = await _dbContext.Messages.AddAsync(messageEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Message> GetById(int id)
    {
        var messageEntity = await _dbContext
            .Messages.AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        return _mapper.Map<Message>(messageEntity);
    }

    public async Task<List<Message>> GetAllByUser(int userId, bool? isRead)
    {
        IQueryable<MessageEntity> query = _dbContext.Messages.AsNoTracking();

        if (isRead == null)
        {
            query = query.Where(m => m.RecepientId == userId && m.RecepientRole == "user" || m.SenderUserId == userId);
        } 
        else
        {
            query = query.Where(m => m.RecepientId == userId && m.RecepientRole == "user" || m.SenderUserId == userId)
                         .Where(m => m.IsRead == isRead);
        }

        query = query.Include(m => m.SenderModerator)
                     .Include(m => m.SenderAdmin);

        var messageEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByModerator(int moderatorId, bool? isRead)
    {
        IQueryable<MessageEntity> query = _dbContext.Messages.AsNoTracking();

        if (isRead == null)
        {
            query = query.Where(m => m.RecepientId == moderatorId && m.RecepientRole == "moderator" || m.SenderModeratorId == moderatorId);
        }
        else
        {
            query = query.Where(m => m.RecepientId == moderatorId && m.RecepientRole == "moderator" || m.SenderModeratorId == moderatorId)
                         .Where(m => m.IsRead == isRead);
        }

        query = query.Include(m => m.SenderUser)
                     .Include(m => m.SenderAdmin);

        var messageEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByAdmin(int adminId, bool? isRead)
    {
        IQueryable<MessageEntity> query = _dbContext.Messages.AsNoTracking();

        if (isRead == null)
        {
            query = query.Where(m => m.RecepientId == adminId && m.RecepientRole == "admin" || m.SenderAdminId == adminId);
        }
        else
        {
            query = query.Where(m => m.RecepientId == adminId && m.RecepientRole == "admin" || m.SenderAdminId == adminId)
                         .Where(m => m.IsRead == isRead);
        }

        query = query.Include(m => m.SenderUser)
                     .Include(m => m.SenderModerator);

        var messageEntitiesList = await query.ToListAsync();

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
