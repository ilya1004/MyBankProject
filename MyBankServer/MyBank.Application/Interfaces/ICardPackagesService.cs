namespace MyBank.Application.Interfaces;

public interface ICardPackagesService
{
    Task<ServiceResponse<int>> Add(CardPackage cardPackage);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CardPackage>>> GetAll();
    Task<ServiceResponse<CardPackage>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll);
}