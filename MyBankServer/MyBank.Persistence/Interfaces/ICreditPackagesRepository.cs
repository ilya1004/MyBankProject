namespace MyBank.Persistence.Interfaces
{
    public interface ICreditPackagesRepository
    {
        Task<int> Add(CreditPackage creditPackage);
        Task<bool> Delete(int id);
        Task<List<CreditPackage>> GetAll(bool includeData);
        Task<CreditPackage> GetById(int id, bool includeData);
        Task<bool> UpdateInfo(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate, string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, int currencyId);
        Task<bool> UpdateStatus(int id, bool isActive);
    }
}