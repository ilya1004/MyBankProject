using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface IDepositAccountsRepository
{
    Task<int> Add(DepositAccount depositAccount, int userId, int currencyId);

    Task<DepositAccount> GetById(int id);

    Task<List<DepositAccount>> GetAllByUser(int userId);

    Task<bool> UpdateInfo(int id, string name, bool isActive);

    Task<bool> UpdateBalance(int id, decimal delta);

    Task<bool> Delete(int id);
}