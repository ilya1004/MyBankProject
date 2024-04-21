using System.Xml.Linq;

namespace MyBank.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public UsersRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);

        var item = await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<User> GetById(int id, bool includeData)
    {
        UserEntity? userEntity = null;
        if (includeData)
        {
            userEntity = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Cards)
                .Include(u => u.PersonalAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.CreditAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.DepositAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.CreditRequests)
                .Include(u => u.Messages)
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive == true);
        }
        else
        {
            userEntity = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        return _mapper.Map<User>(userEntity);
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive == true);

        return _mapper.Map<User>(userEntity);
    }

    public async Task<List<User>> GetAll(bool includeData)
    {
        var userEntitiesList = includeData == true ? 
            await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.PersonalAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.CreditAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.DepositAccounts)
                    .ThenInclude(pa => pa.Currency)
                .Include(u => u.Cards)
                    .ThenInclude(c => c.PersonalAccount)
                        .ThenInclude(pa => pa!.Currency)
                .Include(u => u.CreditPayments)
                    .ThenInclude(cp => cp.CreditAccount)
                .Include(u => u.CreditRequests)
                .Include(u => u.Messages)
                .ToListAsync() :
            await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();

        return _mapper.Map<List<User>>(userEntitiesList);
    }

    public async Task<bool> IsExistByEmail(string email)
    {
        return await _dbContext.Users
            .AnyAsync(u => u.Email == email && u.IsActive == true);
    }

    public async Task<bool> UpdatePassword(int id, string hashedPassword)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.HashedPassword, hashedPassword));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateEmail(int id, string email)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.Email, email));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Nickname, nickname)
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.Surname, surname)
                .SetProperty(u => u.Patronymic, patronymic)
                .SetProperty(u => u.PhoneNumber, phoneNumber)
                .SetProperty(u => u.PassportSeries, passportSeries)
                .SetProperty(u => u.PassportNumber, passportNumber)
                .SetProperty(u => u.Citizenship, citizenship));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateAvatarPath(int id, string fileFullPath)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.AvatarImagePath, fileFullPath));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.IsActive, isActive));

        return (number == 0) ? false : true;
    }


    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
