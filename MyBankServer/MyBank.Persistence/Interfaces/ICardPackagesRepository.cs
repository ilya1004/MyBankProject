

using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface ICardPackagesRepository
{
    Task<int> Add(CardPackage cardPackage);

    Task<CardPackage> GetById(int id);

    Task<List<CardPackage>> GetAll();

    Task<bool> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll);

    Task<bool> Delete(int id);
}