namespace MyBank.Persistence.Repositories;

public class ModeratorsRepository : IModeratorsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public ModeratorsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Moderator moderator)
    {
        var moderatorEntity = _mapper.Map<ModeratorEntity>(moderator);

        var item = await _dbContext.Moderators.AddAsync(moderatorEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Moderator> GetByLogin(string login)
    {
        var moderatorEntity = await _dbContext
            .Moderators.AsNoTracking()
            .FirstOrDefaultAsync(m => m.Login == login);

        return _mapper.Map<Moderator>(moderatorEntity);
    }

    public async Task<Moderator> GetById(int id, bool includeData)
    {
        var moderatorEntity = includeData == true ? 
            await _dbContext.Moderators
                .AsNoTracking()
                .Include(m => m.Messages)
                .Include(m => m.CreditRequestsReplied)
                .Include(m => m.CreditsApproved)
                .FirstOrDefaultAsync(m => m.Id == id) :
            await _dbContext.Moderators
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

        return _mapper.Map<Moderator>(moderatorEntity);
    }

    public async Task<List<Moderator>> GetAll(bool includeData, bool onlyActive)
    {
        IQueryable<ModeratorEntity> query = _dbContext.Moderators;

        if (includeData)
        {
            query = query.Include(m => m.Messages)
                         .Include(m => m.CreditRequestsReplied)
                         .Include(m => m.CreditsApproved);
        }

        if (onlyActive)
        {
            query = query.Where(m => m.IsActive == true);
        }

        var moderatorEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<Moderator>>(moderatorEntitiesList);
    }

    public async Task<bool> IsExistByLogin(string login)
    {
        return await _dbContext.Moderators.AnyAsync(m => m.Login == login);
    }

    public async Task<bool> UpdateInfo(int id, string nickname)
    {
        var number = await _dbContext
            .Moderators.Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(m => m.Nickname, nickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateInfo(int id, bool isActive)
    {
        var number = await _dbContext
            .Moderators.Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext
            .Moderators.Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Moderators.Where(m => m.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
