using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

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
        var cardPackageEntity = new CardPackageEntity
        {
            Id = 0,
            Name = cardPackage.Name,
            Price = cardPackage.Price,
            OperationsNumber = cardPackage.OperationsNumber,
            OperationsSum = cardPackage.OperationsSum,
            AverageAccountBalance = cardPackage.AverageAccountBalance,
            MonthPayroll = cardPackage.MonthPayroll,
            Cards = []
        };

        await _dbContext.CardPackages.AddAsync(cardPackageEntity);
        await _dbContext.SaveChangesAsync();
        return cardPackageEntity.Id;
    }

    public async Task<CardPackage> GetById(int id)
    {
        var cardPackageEntity = await _dbContext.CardPackages
            .AsNoTracking()
            .FirstOrDefaultAsync(cp => cp.Id == id);

        return _mapper.Map<CardPackage>(cardPackageEntity);
    }

    public async Task<List<CardPackage>> GetAll()
    {
        var cardPackageEntitiesList = await _dbContext.CardPackages
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<CardPackage>>(cardPackageEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll)
    {
        var number = await _dbContext.CardPackages
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.Name, name)
                .SetProperty(cp => cp.Price, price)
                .SetProperty(cp => cp.OperationsNumber, operationsNumber)
                .SetProperty(cp => cp.OperationsSum, operationsSum)
                .SetProperty(cp => cp.AverageAccountBalance, averageAccountBalance)
                .SetProperty(cp => cp.MonthPayroll, monthPayroll));

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