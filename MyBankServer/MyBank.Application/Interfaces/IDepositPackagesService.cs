namespace MyBank.Application.Interfaces;

public interface IDepositPackagesService
{
    Task<ServiceResponse<int>> Add(string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, 
        bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<DepositPackage>>> GetAll(bool includeData, bool onlyActive);
    Task<ServiceResponse<DepositPackage>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
}
