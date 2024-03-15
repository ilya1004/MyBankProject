using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ICreditAccountsService
    {
        Task<int> Add(CreditAccount creditAccount, int userId, int currencyId, int moderatorId);
        Task<bool> Delete(int id);
        Task<List<CreditAccount>> GetAllByUser(int userId);
        Task<CreditAccount> GetById(int id);
        Task<bool> UpdateBalance(int id, decimal deltaNumber);
        Task<bool> UpdateInfo(int id, string name, bool isActive);
        Task<bool> UpdatePaymentNumber(int id, int deltaNumber);
    }
}