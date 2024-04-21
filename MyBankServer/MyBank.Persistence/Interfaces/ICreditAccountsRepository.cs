namespace MyBank.Persistence.Interfaces;

public interface ICreditAccountsRepository
{
    Task<int> Add(CreditAccount creditAccount);
    Task<CreditAccount> GetById(int id, bool includeData);
    Task<List<CreditAccount>> GetAllByUser(int userId, bool includeData);
    Task<bool> UpdateInfo(int id, string name, bool isActive);
    Task<bool> UpdateBalance(int id, decimal deltaNumber);
    Task<bool> UpdatePaymentNumber(int id, int deltaNumber);
    Task<bool> Delete(int id);
}