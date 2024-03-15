using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IDepositAccountsService
    {
        Task<int> Add(DepositAccount depositAccount, int userId, int currencyId);
        Task<bool> Delete(int id);
        Task<List<DepositAccount>> GetAllByUser(int userId);
        Task<DepositAccount> GetById(int id);
        Task<bool> UpdateBalance(int id, decimal deltaNumber);
        Task<bool> UpdateName(int id, string name, bool isActive);
    }
}