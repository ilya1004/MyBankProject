using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ICardPackagesService
    {
        Task<int> Add(CardPackage cardPackage);
        Task<bool> Delete(int id);
        Task<List<CardPackage>> GetAll();
        Task<CardPackage> GetById(int id);
        Task<bool> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll);
    }
}