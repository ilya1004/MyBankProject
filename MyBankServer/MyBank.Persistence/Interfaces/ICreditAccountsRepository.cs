namespace MyBank.Persistence.Interfaces;

public interface ICreditAccountsRepository
{
    Task<int> Add(CreditAccount creditAccount);
    Task<CreditAccount> GetById(int id, bool includeData);
    Task<List<CreditAccount>> GetAllByUser(int userId, bool includeData, bool onlyActive);
    Task<bool> UpdateName(int id, string name);
    Task<bool> UpdateBalanceDelta(int id, decimal deltaNumber);
    Task<bool> UpdatePaymentNumber(int id, int deltaNumber);
    Task<bool> Delete(int id);
    Task<bool> SetAccountNumber(int id, string accNumber);
    Task<bool> UpdateStatus(int id, bool isActive);
}