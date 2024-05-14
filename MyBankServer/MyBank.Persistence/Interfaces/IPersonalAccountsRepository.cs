namespace MyBank.Persistence.Interfaces;

public interface IPersonalAccountsRepository
{
    Task<int> Add(PersonalAccount personalAccount);
    Task<PersonalAccount> GetById(int id, bool includeData);
    Task<PersonalAccount> IsOwned(int userId, int personalAccountId);
    Task<bool> IsExist(string accountNumber);
    Task<PersonalAccount> GetByNumber(string personalAccountNumber, bool withUser);
    Task<List<PersonalAccount>> GetAllByUser(int userId, bool includeData);
    Task<List<PersonalAccount>> GetAllByUser(string userNickname, bool includeData);
    Task<bool> SetAccountNumber(int id, string accNumber);
    Task<bool> UpdateBalanceDelta(int id, decimal deltaNumber);
    Task<bool> UpdateBalanceDelta(string accountNumber, decimal deltaNumber);
    Task<bool> Delete(int id);
    Task<bool> UpdateName(int id, string name);
    Task<bool> UpdateStatus(int id, bool isActive);
    Task<bool> UpdateTransfersStatus(int id, bool isForTransfersByNickname);
    Task<bool> UpdateClosingInfo(int id, DateTime dateTime, bool isActive, bool isForTransfersByNickname);
    Task<PersonalAccount> GetIsForTransfersByNickname(string user);
}
