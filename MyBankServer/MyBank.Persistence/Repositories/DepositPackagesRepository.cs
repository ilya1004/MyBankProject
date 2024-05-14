using MyBank.Domain.Models;

namespace MyBank.Persistence.Repositories;

public class DepositPackagesRepository : IDepositPackagesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public DepositPackagesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(DepositPackage depositPackage)
    {
        var currencyEntity = await _dbContext.Currencies
            .FirstOrDefaultAsync(cp => cp.Id == depositPackage.CurrencyId);

        var depositPackageEntity = _mapper.Map<DepositPackageEntity>(depositPackage);

        depositPackageEntity.Currency = currencyEntity;

        var item = await _dbContext.DepositPackages.AddAsync(depositPackageEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<DepositPackage> GetById(int id, bool includeData)
    {
        IQueryable<DepositPackageEntity> query = _dbContext.DepositPackages.AsNoTracking();

        if (includeData)
        {
            query = query.Include(cp => cp.Currency);
        }

        var depositPackageEntity = await query.FirstOrDefaultAsync(cp => cp.Id == id && cp.IsActive == true);

        return _mapper.Map<DepositPackage>(depositPackageEntity);
    }

    public async Task<List<DepositPackage>> GetAll(bool includeData, bool onlyActive)
    {
        IQueryable<DepositPackageEntity> query = _dbContext.DepositPackages.AsNoTracking();

        if (includeData)
        {
            query = query.Include(dp => dp.Currency);
        }

        if (onlyActive)
        {
            query = query.Where(dp => dp.IsActive == true);
        }

        var depositPackageEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<DepositPackage>>(depositPackageEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId)
    {
        var number = await _dbContext.DepositPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.Name, name)
                .SetProperty(cp => cp.DepositStartBalance, depositStartBalance)
                .SetProperty(cp => cp.InterestRate, interestRate)
                .SetProperty(cp => cp.DepositTermInDays, depositTermInDays)
                .SetProperty(cp => cp.IsRevocable, isRevocable)
                .SetProperty(cp => cp.HasCapitalisation, hasCapitalisation)
                .SetProperty(cp => cp.HasInterestWithdrawalOption, hasInterestWithdrawalOption)
                .SetProperty(cp => cp.CurrencyId, currencyId));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.DepositPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.DepositPackages
            .Where(cp => cp.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
