
namespace MyBank.Persistence.Repositories;

public class BankSettingsRepository : IBankSettingsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public BankSettingsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BankSettings> GetById(int id)
    {
        var bankSettingsEntity = await _dbContext.BankSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(bs => bs.Id == id);

        return _mapper.Map<BankSettings>(bankSettingsEntity);
    }

    public async Task<bool> Update(int id, decimal creditInterestRate, decimal depositInterestRate)
    {
        var number = await _dbContext.BankSettings
            .Where(bs => bs.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(bs => bs.CreditInterestRate, creditInterestRate)
                .SetProperty(bs => bs.DepositInterestRate, depositInterestRate));

        return (number == 0) ? false : true;
    }
}
