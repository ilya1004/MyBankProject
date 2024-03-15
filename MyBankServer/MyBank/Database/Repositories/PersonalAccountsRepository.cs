using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Abstractions;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class PersonalAccountsRepository : IPersonalAccountsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public PersonalAccountsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(PersonalAccount personalAccount, int userId, int currencyId)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var currencyEntity = await _dbContext.Currencies
            .FirstOrDefaultAsync(c => c.Id == currencyId);

        var personalAccountEntity = new PersonalAccountEntity
        {
            Id = 0,
            Name = personalAccount.Name,
            Number = personalAccount.Number,
            CurrentBalance = personalAccount.CurrentBalance,
            CreationDate = personalAccount.CreationDate,
            ClosingDate = personalAccount.ClosingDate,
            IsActive = personalAccount.IsActive,
            IsForTransfersByNickname = personalAccount.IsForTransfersByNickname,
            UserId = userId,
            UserOwner = userEntity,
            CurrencyId = currencyId,
            Currency = currencyEntity,
            Transactions = [],
            Cards = []
        };

        var item = await _dbContext.PersonalAccounts.AddAsync(personalAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<PersonalAccount> GetById(int id)
    {
        var personalAccountEntity = await _dbContext.PersonalAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(pa => pa.Id == id);

        return _mapper.Map<PersonalAccount>(personalAccountEntity);
    }

    public async Task<List<PersonalAccount>> GetAllByUser(int userId)
    {
        var personalAccountEntitiesList = await _dbContext.PersonalAccounts
            .AsNoTracking()
            .Where(pa => pa.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<PersonalAccount>>(personalAccountEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname)
    {
        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(pa => pa.Name, name)
                .SetProperty(pa => pa.IsActive, isActive)
                .SetProperty(pa => pa.IsForTransfersByNickname, isForTransfersByNickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(pa => pa.CurrentBalance, pa => pa.CurrentBalance + deltaNumber));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.PersonalAccounts
            .Where(pa => pa.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
