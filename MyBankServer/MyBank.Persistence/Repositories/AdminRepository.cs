namespace MyBank.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdminRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Admin admin)
    {
        var adminEntity = _mapper.Map<AdminEntity>(admin);

        var item = await _dbContext.Admins.AddAsync(adminEntity);
        var number = await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Admin> GetById(int id)
    {
        var adminEntity = await _dbContext
            .Admins.AsNoTracking()
            .FirstOrDefaultAsync(а => а.Id == id);

        return _mapper.Map<Admin>(adminEntity);
    }

    public async Task<Admin> GetByLogin(string login)
    {
        var adminEntity = await _dbContext
            .Admins.AsNoTracking()
            .FirstOrDefaultAsync(а => а.Login == login);

        return _mapper.Map<Admin>(adminEntity);
    }

    public async Task<List<Admin>> GetAll()
    {
        var adminEntitiesList = await _dbContext.Admins.AsNoTracking().ToListAsync();

        return _mapper.Map<List<Admin>>(adminEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string nickname)
    {
        var number = await _dbContext
            .Admins.Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.Nickname, nickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Admins.Where(a => a.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }

    public async Task<bool> IsExistByLogin(string login)
    {
        return await _dbContext.Admins.AnyAsync(a => a.Login == login);
    }
}
