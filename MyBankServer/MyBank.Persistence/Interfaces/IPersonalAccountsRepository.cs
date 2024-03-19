using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface IPersonalAccountsRepository
{
    Task<int> Add(PersonalAccount personalAccount, int userId, int currencyId);

    Task<PersonalAccount> GetById(int id);

    Task<List<PersonalAccount>> GetAllByUser(int userId);

    Task<bool> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname);

    Task<bool> UpdateBalance(int id, decimal newBalance);

    Task<bool> Delete(int id);
}