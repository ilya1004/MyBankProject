using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;
    public TransactionsService(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<int> Add(Transaction transaction, int personalAccountId)
    {
        return await _transactionsRepository.Add(transaction, personalAccountId);
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountId(int personalAccountId)
    {
        return await _transactionsRepository.GetAllByPersonalAccountId(personalAccountId);
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd)
    {
        return await _transactionsRepository.GetAllByPersonalAccountDate(personalAccountId, dateTimeStart, dateTimeEnd);
    }
}
