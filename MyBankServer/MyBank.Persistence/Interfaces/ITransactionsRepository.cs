using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface ITransactionsRepository
{
    Task<int> Add(Transaction transaction, int personalAccountId);

    Task<List<Transaction>> GetAllByPersonalAccountId(int personalAccountId);

    Task<List<Transaction>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd);
}