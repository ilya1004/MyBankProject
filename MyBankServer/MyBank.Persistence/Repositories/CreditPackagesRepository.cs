using MyBank.Domain.Models;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Repositories;

public class CreditPackagesRepository : ICreditPackagesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreditPackagesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditPackage creditPackage)
    {
        var currencyEntity = await _dbContext.Currencies
            .FirstOrDefaultAsync(cp => cp.Id == creditPackage.CurrencyId);

        var creditPackageEntity = _mapper.Map<CreditPackageEntity>(creditPackage);

        creditPackageEntity.Currency = currencyEntity;

        var item = await _dbContext.CreditPackages.AddAsync(creditPackageEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditPackage> GetById(int id, bool includeData)
    {
        IQueryable<CreditPackageEntity> query = _dbContext.CreditPackages.AsNoTracking();
        
        if (includeData)
        {
            query = query.Include(cp => cp.Currency);
        }

        var creditPackageEntity = await query.FirstOrDefaultAsync(cp => cp.Id == id && cp.IsActive == true);

        return _mapper.Map<CreditPackage>(creditPackageEntity);
    }

    public async Task<List<CreditPackage>> GetAll(bool includeData)
    {
        IQueryable<CreditPackageEntity> query = _dbContext.CreditPackages.AsNoTracking();

        if (includeData)
        {
            query = query.Include(cp => cp.Currency);
        }

        query = query.Where(cp => cp.IsActive == true);

        var creditPackageEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<CreditPackage>>(creditPackageEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate,
        string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, int currencyId)
    {
        var number = await _dbContext.CreditPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.Name, name)
                .SetProperty(cp => cp.CreditStartBalance, creditStartBalance)
                .SetProperty(cp => cp.CreditGrantedAmount, creditGrantedAmount)
                .SetProperty(cp => cp.InterestRate, interestRate)
                .SetProperty(cp => cp.InterestCalculationType, interestCalculationType)
                .SetProperty(cp => cp.CreditTermInDays, creditTermInDays)
                .SetProperty(cp => cp.HasPrepaymentOption, hasPrepaymentOption)
                .SetProperty(cp => cp.CurrencyId, currencyId));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.CreditPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditPackages
            .Where(cp => cp.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
