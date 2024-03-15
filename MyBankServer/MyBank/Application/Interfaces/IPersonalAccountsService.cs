using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IPersonalAccountsService
    {
        Task<int> Add(PersonalAccount personalAccount, int userId, int currencyId);
        Task<bool> Delete(int id);
        Task<List<PersonalAccount>> GetAllByUser(int userId);
        Task<PersonalAccount> GetById(int id);
        Task<bool> UpdateBalance(int id, decimal deltaNumber);
        Task<bool> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname);
    }
}