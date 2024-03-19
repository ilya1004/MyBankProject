using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Entities;

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

    public async Task<int> Add(DepositAccount depositAccount, int userId, int currencyId)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var currencyEntity = await _dbContext.Currencies
            .FirstOrDefaultAsync(c => c.Id == currencyId);

        var depositAccountEntity = new DepositAccountEntity
        {
            Id = 0,
            Name = depositAccount.Name,
            Number = depositAccount.Number,
            CurrentBalance = depositAccount.CurrentBalance,
            DepositStartBalance = depositAccount.DepositStartBalance,
            CreationDate = depositAccount.CreationDate,
            IsActive = depositAccount.IsActive,
            InterestRate = depositAccount.InterestRate,
            DepositTermInDays = depositAccount.DepositTermInDays,
            TotalAccrualsNumber = depositAccount.TotalAccrualsNumber,
            MadeAccrualsNumber = depositAccount.MadeAccrualsNumber,
            IsRevocable = depositAccount.IsRevocable,
            HasCapitalisation = depositAccount.HasCapitalisation,
            HasInterestWithdrawalOption = depositAccount.HasInterestWithdrawalOption,
            UserId = userId,
            UserOwner = userEntity,
            CurrencyId = currencyId,
            Currency = currencyEntity,
            Accruals = []
        };

        var item = await _dbContext.DepositAccounts.AddAsync(depositAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<DepositAccount> GetById(int id)
    {
        var depositAccountEntity = await _dbContext.DepositAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(da => da.Id == id);

        return _mapper.Map<DepositAccount>(depositAccountEntity);
    }

    public async Task<List<DepositAccount>> GetAllByUser(int userId)
    {
        var depositAccountEntitiesList = await _dbContext.DepositAccounts
            .AsNoTracking()
            .Where(da => da.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<DepositAccount>>(depositAccountEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive)
    {
        var number = await _dbContext.DepositAccounts
            .Where(da => da.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(da => da.Name, name)
                .SetProperty(da => da.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        var number = await _dbContext.DepositAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(pa => pa.CurrentBalance, pa => pa.CurrentBalance + deltaNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.DepositAccounts
            .Where(da => da.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
