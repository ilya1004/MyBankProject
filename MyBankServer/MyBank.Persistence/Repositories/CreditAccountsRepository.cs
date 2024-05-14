using MyBank.Domain.Models;

namespace MyBank.Persistence.Repositories;

public class CreditAccountsRepository : ICreditAccountsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreditAccountsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditAccount creditAccount)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.Id == creditAccount.UserId
        );

        var currencyEntity = await _dbContext.Currencies.FirstOrDefaultAsync(c =>
            c.Id == creditAccount.CurrencyId
        );

        var moderatorEntity = await _dbContext.Moderators.FirstOrDefaultAsync(m =>
            m.Id == creditAccount.ModeratorApprovedId
        );

        var creditAccountEntity = _mapper.Map<CreditAccountEntity>(creditAccount);

        creditAccountEntity.User = userEntity;
        creditAccountEntity.Currency = currencyEntity;
        creditAccountEntity.ModeratorApproved = moderatorEntity;

        var item = await _dbContext.CreditAccounts.AddAsync(creditAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditAccount> GetById(int id, bool includeData)
    {
        var creditAccountEntity = includeData == true ?
            await _dbContext.CreditAccounts
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Currency)
                .Include(c => c.ModeratorApproved)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(ca => ca.Id == id) :
            await _dbContext.CreditAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(ca => ca.Id == id);

        return _mapper.Map<CreditAccount>(creditAccountEntity);
    }

    public async Task<List<CreditAccount>> GetAllByUser(int userId, bool includeData, bool onlyActive)
    {
        IQueryable<CreditAccountEntity> query = _dbContext.CreditAccounts.AsNoTracking();

        if (includeData)
        {
            query = query.Include(c => c.User)
                         .Include(c => c.Currency)
                         .Include(c => c.ModeratorApproved)
                         .Include(c => c.Payments);
        }

        query = query.Where(c => c.UserId == userId);

        if (onlyActive)
        {
            query = query.Where(c => c.IsActive == true);
        }

        var creditAccountEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<CreditAccount>>(creditAccountEntitiesList);
    }

    public async Task<List<CreditAccount>> GetAll(bool includeData, bool onlyActive)
    {
        IQueryable<CreditAccountEntity> query = _dbContext.CreditAccounts.AsNoTracking();

        if (includeData)
        {
            query = query.Include(c => c.User)
                         .Include(c => c.Currency)
                         .Include(c => c.ModeratorApproved)
                         .Include(c => c.Payments);
        }

        if (onlyActive)
        {
            query = query.Where(c => c.IsActive == true);
        }

        var creditAccountEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<CreditAccount>>(creditAccountEntitiesList);
    }

    public async Task<bool> UpdateName(int id, string name)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(ca => ca.Name, name));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalanceDelta(int id, decimal deltaNumber)
    {
        var creditAccountEntity = await _dbContext.CreditAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(ca => ca.Id == id);

        decimal delta = deltaNumber + creditAccountEntity!.CurrentBalance;

        var number = await _dbContext.CreditAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.CurrentBalance, delta));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateClosingInfo(int id, decimal currentBalance, int madePaymentsNumber, DateTime closingDate, bool isActive)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.CurrentBalance, currentBalance)
                .SetProperty(ca => ca.MadePaymentsNumber, madePaymentsNumber)
                .SetProperty(ca => ca.ClosingDate, closingDate)
                .SetProperty(ca => ca.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdatePaymentNumber(int id, int deltaNumber)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.MadePaymentsNumber, ca => ca.MadePaymentsNumber + deltaNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> SetAccountNumber(int id, string accNumber)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(ca => ca.Number, accNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditAccounts.Where(ca => ca.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
