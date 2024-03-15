using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface ICreditAccountsRepository
{
    Task<int> Add(CreditAccount creditAccount, int userId, int currencyId, int moderatorId);

    Task<CreditAccount> GetById(int id);

    Task<List<CreditAccount>> GetAllByUser(int userId);

    Task<bool> UpdateInfo(int id, string name, bool isActive);

    Task<bool> UpdateBalance(int id, decimal deltaNumber);

    Task<bool> UpdatePaymentNumber(int id, int deltaNumber);

    Task<bool> Delete(int id);
}