using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface IDepositAccountsRepository
{
    Task<int> Add(DepositAccount depositAccount, int userId, int currencyId);

    Task<DepositAccount> GetById(int id);

    Task<List<DepositAccount>> GetAllByUser(int userId);

    Task<bool> UpdateName(int id, string name, bool isActive);

    Task<bool> UpdateBalance(int id, decimal delta);

    Task<bool> Delete(int id);
}