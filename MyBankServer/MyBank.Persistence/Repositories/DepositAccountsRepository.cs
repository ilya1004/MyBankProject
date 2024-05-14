namespace MyBank.Persistence.Repositories;

public class DepositAccountsRepository : IDepositAccountsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public DepositAccountsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(DepositAccount depositAccount)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.Id == depositAccount.UserId
        );

        var currencyEntity = await _dbContext.Currencies.FirstOrDefaultAsync(c =>
            c.Id == depositAccount.CurrencyId
        );

        var depositAccountEntity = _mapper.Map<DepositAccountEntity>(depositAccount);

        depositAccountEntity.User = userEntity;
        depositAccountEntity.Currency = currencyEntity;

        var item = await _dbContext.DepositAccounts.AddAsync(depositAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<DepositAccount> GetById(int id, bool includeData)
    {
        var depositAccountEntity = includeData == true ?
            await _dbContext.DepositAccounts
                .AsNoTracking()
                .Include(da => da.User)
                .Include(da => da.Currency)
                .Include(da => da.Accruals)
                .FirstOrDefaultAsync(da => da.Id == id) :
            await _dbContext.DepositAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(da => da.Id == id);

        return _mapper.Map<DepositAccount>(depositAccountEntity);
    }

    public async Task<List<DepositAccount>> GetAllByUser(int userId, bool includeData, bool onlyActive)
    {
        IQueryable<DepositAccountEntity> query = _dbContext.DepositAccounts.AsNoTracking();

        if (includeData)
        {
            query = query.Include(da => da.User)
                         .Include(da => da.Currency)
                         .Include(da => da.Accruals);
        }

        query = query.Where(da => da.UserId == userId);

        if (onlyActive)
        {
            query = query.Where(da => da.IsActive == true);
        }

        var depositAccountEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<DepositAccount>>(depositAccountEntitiesList);
    }

    public async Task<bool> UpdateName(int id, string name)
    {
        var number = await _dbContext
            .DepositAccounts.Where(da => da.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(da => da.Name, name));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext
            .DepositAccounts.Where(da => da.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(da => da.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalanceDelta(int id, decimal deltaNumber)
    {
        var depositAccountEntity = await _dbContext.DepositAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(da => da.Id == id);

        decimal delta = deltaNumber + depositAccountEntity!.CurrentBalance;

        var number = await _dbContext.DepositAccounts
            .Where(da => da.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(pa => pa.CurrentBalance, delta));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalanceValue(int id, decimal newBalance)
    {
        var number = await _dbContext
            .DepositAccounts.Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(pa => pa.CurrentBalance, newBalance));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateClosingInfo(int id, int newBalance, DateTime dateTime, bool isActive)
    {
        var number = await _dbContext
            .DepositAccounts.Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(pa => pa.CurrentBalance, newBalance)
                    .SetProperty(pa => pa.ClosingDate, dateTime)
                    .SetProperty(pa => pa.IsActive, isActive)
            );

        return (number == 0) ? false : true;
    }
    public async Task<bool> SetAccountNumber(int id, string accNumber)
    {
        var number = await _dbContext.DepositAccounts
            .Where(da => da.Id == id)
            .ExecuteUpdateAsync(s =>s
                .SetProperty(da => da.Number, accNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.DepositAccounts.Where(da => da.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
