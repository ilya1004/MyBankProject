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

        if (message.SenderAdminId != -1)
        {
            messageEntity.SenderAdmin = await _dbContext.Admins.FirstOrDefaultAsync(a =>
                a.Id == message.SenderAdminId
            );
        }
        else if (message.SenderModeratorId != -1)
        {
            messageEntity.SenderModerator = await _dbContext.Moderators.FirstOrDefaultAsync(m =>
                m.Id == message.SenderModeratorId
            );
        }
        else if (message.SenderUserId != -1)
        {
            messageEntity.SenderUser = await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.Id == message.SenderUserId
            );
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

    public async Task<List<Message>> GetAllByUser(int userId)
    {
        var messageEntitiesList = await _dbContext
            .Messages.AsNoTracking()
            .Where(m => m.SenderUserId == userId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByModerator(int moderatorId)
    {
        var messageEntitiesList = await _dbContext
            .Messages.AsNoTracking()
            .Where(m => m.SenderModeratorId == moderatorId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<List<Message>> GetAllByAdmin(int adminId)
    {
        var messageEntitiesList = await _dbContext
            .Messages.AsNoTracking()
            .Where(m => m.SenderAdminId == adminId)
            .ToListAsync();

        return _mapper.Map<List<Message>>(messageEntitiesList);
    }

    public async Task<bool> UpdateIsRead(int id, bool isRead)
    {
        var number = await _dbContext
            .Messages.Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsRead, isRead));

        return (number == 0) ? false : true;
    }
}
