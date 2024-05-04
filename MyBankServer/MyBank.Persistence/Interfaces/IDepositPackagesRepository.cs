namespace MyBank.Persistence.Interfaces;

public interface IDepositPackagesRepository
{
    Task<int> Add(DepositPackage depositPackage);
    Task<bool> Delete(int id);
    Task<List<DepositPackage>> GetAll(bool includeData);
    Task<DepositPackage> GetById(int id, bool includeData);
    Task<bool> UpdateInfo(int id, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId);
    Task<bool> UpdateStatus(int id, bool isActive);
}