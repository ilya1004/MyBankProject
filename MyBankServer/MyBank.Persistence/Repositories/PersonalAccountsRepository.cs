using System.Data.SqlTypes;

namespace MyBank.Persistence.Repositories;

public class PersonalAccountsRepository : IPersonalAccountsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public PersonalAccountsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(PersonalAccount personalAccount)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.Id == personalAccount.UserId
        );

        var currencyEntity = await _dbContext.Currencies.FirstOrDefaultAsync(c =>
            c.Id == personalAccount.CurrencyId
        );

        var personalAccountEntity = _mapper.Map<PersonalAccountEntity>(personalAccount);

        personalAccountEntity.User = userEntity;
        personalAccountEntity.Currency = currencyEntity;

        var item = await _dbContext.PersonalAccounts.AddAsync(personalAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<PersonalAccount> IsOwned(int userId, int personalAccountId)
    {
        var personalAccountEntity = await _dbContext.PersonalAccounts
            .AnyAsync(pa => pa.Id == personalAccountId && pa.UserId == userId);

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }

    public async Task<PersonalAccount> GetById(int id)
    {
        var personalAccountEntity = await _dbContext
            .PersonalAccounts.AsNoTracking()
            .FirstOrDefaultAsync(pa => pa.Id == id);

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }

    public async Task<PersonalAccount> GetById(int id, bool includeData)
    {
        var personalAccountEntity = includeData == true ?
            await _dbContext.PersonalAccounts
                .AsNoTracking()
                .Include(pa => pa.Cards)
                .Include(pa => pa.Currency)
                .Include(pa => pa.User)
                .FirstOrDefaultAsync(pa => pa.Id == id) :
            await _dbContext.PersonalAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(pa => pa.Id == id);

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }

    public async Task<PersonalAccount> GetByNumber(string personalAccountNumber, bool withUser)
    {
        PersonalAccountEntity? personalAccountEntity = null;
        if (withUser)
        {
            personalAccountEntity = await _dbContext
                .PersonalAccounts.AsNoTracking()
                .Include(pa => pa.User)
                .FirstOrDefaultAsync(pa => pa.Number == personalAccountNumber);
        }
        else
        {
            personalAccountEntity = await _dbContext
                .PersonalAccounts.AsNoTracking()
                .FirstOrDefaultAsync(pa => pa.Number == personalAccountNumber);
        }

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }

    public async Task<List<PersonalAccount>> GetAllByUser(int userId, bool includeData)
    {
        var personalAccountEntitiesList = includeData == true ?
            await _dbContext.PersonalAccounts
                .AsNoTracking()
                .Where(pa => pa.IsActive == true && pa.UserId == userId)
                .Include(pa => pa.Currency)
                .ToListAsync() :
            await _dbContext.PersonalAccounts
                .AsNoTracking()
                .Where(pa => pa.UserId == userId)
                .ToListAsync();

        return _mapper.Map<List<PersonalAccount>>(personalAccountEntitiesList);
    }

    public async Task<bool> SetAccountNumber(int id, string accNumber)
    {
        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(pa => pa.Number, accNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalanceDelta(int id, decimal deltaNumber)
    {
        var personalAccountEntity = await _dbContext.PersonalAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(pa => pa.Id == id);

        decimal delta = deltaNumber + personalAccountEntity!.CurrentBalance;

        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(pa => pa.CurrentBalance, delta));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalanceDelta(string accountNumber, decimal deltaNumber)
    {
        var number = await _dbContext
            .PersonalAccounts.Where(pa => pa.Number == accountNumber)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(pa => pa.CurrentBalance, pa => pa.CurrentBalance + deltaNumber)
            );

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext
            .PersonalAccounts.Where(pa => pa.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateName(int id, string name)
    {
        var number = await _dbContext
            .PersonalAccounts.Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(pa => pa.Name, name));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext
            .PersonalAccounts.Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(pa => pa.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateTransfersStatus(int id, bool isForTransfersByNickname)
    {
        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(pa => pa.IsForTransfersByNickname, isForTransfersByNickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateClosingInfo(int id, DateTime dateTime, bool isActive, bool isForTransfersByNickname)
    {
        var number = await _dbContext
            .PersonalAccounts.Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(pa => pa.ClosingDate, dateTime)
                    .SetProperty(pa => pa.IsActive, isActive)
                    .SetProperty(pa => pa.IsForTransfersByNickname, isForTransfersByNickname)
            );

        return (number == 0) ? false : true;
    }

    public async Task<PersonalAccount> GetIsForTransfersByNickname(string userRecipientNickname)
    {
        var userEntity = await _dbContext
            .Users.AsNoTracking()
            .Include(u => u.PersonalAccounts)
            .FirstOrDefaultAsync(u => u.Nickname == userRecipientNickname);

        PersonalAccountEntity? personalAccountEntity = null;

        foreach (var account in userEntity!.PersonalAccounts)
        {
            if (account.IsActive && account.IsForTransfersByNickname)
            {
                personalAccountEntity = account;
            }
        }

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }
}
