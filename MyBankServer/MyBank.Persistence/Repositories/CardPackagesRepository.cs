namespace MyBank.Persistence.Repositories;

public class CardPackagesRepository : ICardPackagesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public CardPackagesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CardPackage cardPackage)
    {
        var cardPackageEntity = _mapper.Map<CardPackageEntity>(cardPackage);

        var item = await _dbContext.CardPackages.AddAsync(cardPackageEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CardPackage> GetById(int id)
    {
        var cardPackageEntity = await _dbContext.CardPackages
            .AsNoTracking()
            .FirstOrDefaultAsync(cp => cp.Id == id && cp.IsActive == true);

        return _mapper.Map<CardPackage>(cardPackageEntity);
    }

    public async Task<List<CardPackage>> GetAll()
    {
        var cardPackageEntitiesList = await _dbContext.CardPackages
            .AsNoTracking()
            .Where(cp => cp.IsActive == true)
            .ToListAsync();

        return _mapper.Map<List<CardPackage>>(cardPackageEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll)
    {
        var number = await _dbContext.CardPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s =>s
                .SetProperty(cp => cp.Name, name)
                .SetProperty(cp => cp.Price, price)
                .SetProperty(cp => cp.OperationsNumber, operationsNumber)
                .SetProperty(cp => cp.OperationsSum, operationsSum)
                .SetProperty(cp => cp.AverageAccountBalance, averageAccountBalance));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.CardPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(cp => cp.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CardPackages
            .Where(cp => cp.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
