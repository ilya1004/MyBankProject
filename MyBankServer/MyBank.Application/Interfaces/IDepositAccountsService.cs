namespace MyBank.Application.Interfaces;

public interface IDepositAccountsService
{
    //Task<ServiceResponse<int>> Add(DepositAccount depositAccount);
    Task<ServiceResponse<int>> Add(int value, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<DepositAccount>>> GetAllByUser(int userId, bool includeData, bool onlyActive);
    Task<ServiceResponse<DepositAccount>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateBalanceDelta(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateBalanceValue(int id, decimal newBalance);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
    Task<ServiceResponse<bool>> RevokeDeposit(int depositAccountId, int personalAccountId);
    Task<ServiceResponse<bool>> WithdrawInterests(int depositAccountId, int personalAccountId);
}
