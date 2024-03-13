using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class CurrenciesRepository : ICurrenciesRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public CurrenciesRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> Add(Currency currency)
    {
        var currencyEntity = new CurrencyEntity
        {
            Id = 0,
            Code = currency.Code,
            Name = currency.Name,
            Scale = currency.Scale,
            LastDateRateUpdate = currency.LastDateRateUpdate,
            OfficialRate = currency.OfficialRate,
            PersonalAccounts = [],
            CreditAccounts = [],
            DepositAccounts = []
        };

        await _dbContext.Currencies.AddAsync(currencyEntity);
        var number = await _dbContext.SaveChangesAsync();
        return number == 0 ? false : true;
    }

    public async Task<Currency> GetById(int id)
    {
        var currencyEntity = await _dbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Currency>(currencyEntity);
    }

    public async Task<Currency> GetByCode(string code)
    {
        var currencyEntity = await _dbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == code);

        return _mapper.Map<Currency>(currencyEntity);
    }

    public async Task<List<Currency>> GetAll()
    {
        var currencyEntitiesList = await _dbContext.Currencies
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Currency>>(currencyEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, DateTime lastDateRateUpdate, decimal officialRate)
    {
        var number = await _dbContext.Currencies
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.LastDateRateUpdate, lastDateRateUpdate)
                .SetProperty(c => c.OfficialRate, officialRate));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Currencies
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
