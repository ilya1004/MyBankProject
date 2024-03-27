namespace MyBank.Persistence.Interfaces;

public interface IDepositAccountsRepository
{
    Task<int> Add(DepositAccount depositAccount);
    Task<DepositAccount> GetById(int id, bool withUser);
    Task<List<DepositAccount>> GetAllByUser(int userId);
    Task<bool> UpdateName(int id, string name);
    Task<bool> UpdateStatus(int id, bool isActive);
    Task<bool> UpdateBalanceDelta(int id, decimal delta);
    Task<bool> UpdateBalanceValue(int id, decimal newBalance);
    Task<bool> Delete(int id);
    Task<bool> UpdateClosingInfo(int id, int newBalance, DateTime dateTime, bool isActive);
}