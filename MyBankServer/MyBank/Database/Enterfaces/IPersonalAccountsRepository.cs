using MyBank.Core.Models;

namespace MyBank.Database.Enterfaces;

public interface IPersonalAccountsRepository
{
    Task<int> Add(PersonalAccount personalAccount, int userId, int currencyId);

    Task<PersonalAccount> GetById(int id);

    Task<List<PersonalAccount>> GetAllByUser(int userId);

    Task<bool> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname);

    Task<bool> UpdateBalance(int id, decimal newBalance);

    Task<bool> Delete(int id);
}