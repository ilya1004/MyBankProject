namespace MyBank.Application.Interfaces;

public interface ICreditPackagesService
{
    Task<ServiceResponse<int>> Add(CreditPackage creditPackage);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditPackage>>> GetAll(bool includeData);
    Task<ServiceResponse<CreditPackage>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate, string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, int currencyId);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
}