using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class CreditAccountsRepository : ICreditAccountsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreditAccountsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditAccount creditAccount, int userId, int currencyId, int moderatorId)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var currencyEntity = await _dbContext.Currencies
            .FirstOrDefaultAsync(c => c.Id == currencyId);

        var moderatorEntity = await _dbContext.Moderators
            .FirstOrDefaultAsync(m => m.Id == moderatorId);

        var creditAccountEntity = new CreditAccountEntity
        {
            Id = 0,
            Name = creditAccount.Name,
            Number = creditAccount.Number,
            CurrentBalance = creditAccount.CurrentBalance,
            CreditStartBalance = creditAccount.CreditStartBalance,
            CreationDate = creditAccount.CreationDate,
            ClosingDate = creditAccount.ClosingDate,
            IsActive = creditAccount.IsActive,
            InterestRate = creditAccount.InterestRate,
            InterestCalculationType = creditAccount.InterestCalculationType,
            CreditTermInDays = creditAccount.CreditTermInDays,
            TotalPaymentsNumber = creditAccount.TotalPaymentsNumber,
            MadePaymentsNumber = creditAccount.MadePaymentsNumber,
            HasPrepaymentOption = creditAccount.HasPrepaymentOption,
            UserId = userId,
            UserOwner = userEntity,
            CurrencyId = currencyId,
            Currency = currencyEntity,
            ModeratorApprovedId = moderatorId,
            ModeratorApproved = moderatorEntity,
            Payments = []
        };

        var item = await _dbContext.CreditAccounts.AddAsync(creditAccountEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditAccount> GetById(int id)
    {
        var creditAccountEntity = await _dbContext.CreditAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(ca => ca.Id == id);

        return _mapper.Map<CreditAccount>(creditAccountEntity);
    }

    public async Task<List<CreditAccount>> GetAllByUser(int userId)
    {
        var creditAccountEntitiesList = await _dbContext.CreditAccounts
            .AsNoTracking()
            .Where(ca => ca.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<CreditAccount>>(creditAccountEntitiesList);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.Name, name)
                .SetProperty(ca => ca.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ca => ca.CurrentBalance, ca => ca.CurrentBalance + deltaNumber));

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

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditAccounts
            .Where(ca => ca.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
