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

    public async Task<List<CreditAccount>> GetAllByUser(int userId, bool includeData)
    {
        var creditAccountEntitiesList = includeData == true ?
            await _dbContext.CreditAccounts
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Currency)
                .Include(c => c.ModeratorApproved)
                .Include(c => c.Payments)
                .Where(ca => ca.UserId == userId)
                .ToListAsync() :
            await _dbContext.CreditAccounts
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Currency)
                .Include(c => c.ModeratorApproved)
                .Include(c => c.Payments)
                .Where(ca => ca.UserId == userId)
                .ToListAsync();

        return _mapper.Map<List<CreditAccount>>(creditAccountEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive)
    {
        var number = await _dbContext
            .CreditAccounts.Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(ca => ca.Name, name).SetProperty(ca => ca.IsActive, isActive)
            );

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        var number = await _dbContext
            .CreditAccounts.Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(ca => ca.CurrentBalance, ca => ca.CurrentBalance + deltaNumber)
            );

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdatePaymentNumber(int id, int deltaNumber)
    {
        var number = await _dbContext
            .CreditAccounts.Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(
                    ca => ca.MadePaymentsNumber,
                    ca => ca.MadePaymentsNumber + deltaNumber
                )
            );

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditAccounts.Where(ca => ca.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
