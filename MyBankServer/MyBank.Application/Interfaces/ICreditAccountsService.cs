namespace MyBank.Application.Interfaces;

public interface ICreditAccountsService
{
    Task<ServiceResponse<int>> Add(CreditAccount creditAccount);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditAccount>>> GetAllByUser(int userId, bool includeData);
    Task<ServiceResponse<CreditAccount>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive);
    Task<ServiceResponse<bool>> UpdatePaymentNumber(int id, int deltaNumber);
}
