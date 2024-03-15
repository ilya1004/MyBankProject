using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public TransactionsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Transaction transaction, int personalAccountId)
    {
        var personalAccountEntity = await _dbContext.PersonalAccounts
                .FirstOrDefaultAsync(pa => pa.Id == personalAccountId);

        var transactionEntity = new TransactionEntity
        {
            Id = 0,
            PaymentAmount = transaction.PaymentAmount,
            Datetime = transaction.Datetime,
            Status = transaction.Status,
            Information = transaction.Information,
            AccountReceiverNumber = transaction.AccountReceiverNumber,
            PersonalAccountId = personalAccountId,
            PersonalAccount = personalAccountEntity
        };

        var item = await _dbContext.Transactions.AddAsync(transactionEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountId(int personalAccountId)
    {
        var transationEntitiesList = await _dbContext.Transactions
            .AsNoTracking()
            .Where(t => t.PersonalAccountId == personalAccountId)
            .ToListAsync();

        return _mapper.Map<List<Transaction>>(transationEntitiesList);
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd)
    {
        var transationEntitiesList = await _dbContext.Transactions
            .AsNoTracking()
            .Where(t => t.PersonalAccountId == personalAccountId
                && dateTimeStart <= t.Datetime && t.Datetime <= dateTimeEnd)
            .ToListAsync();

        return _mapper.Map<List<Transaction>>(transationEntitiesList);
    }
}
