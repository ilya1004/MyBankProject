using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ITransactionsService
    {
        Task<int> Add(Transaction transaction, int personalAccountId);
        Task<List<Transaction>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd);
        Task<List<Transaction>> GetAllByPersonalAccountId(int personalAccountId);
    }
}