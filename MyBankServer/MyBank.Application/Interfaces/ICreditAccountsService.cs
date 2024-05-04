namespace MyBank.Application.Interfaces;

public interface ICreditAccountsService
{
    Task<ServiceResponse<int>> Add(CreditAccount creditAccount);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditAccount>>> GetAllByUser(int userId, bool includeData, bool onlyActive);
    Task<ServiceResponse<CreditAccount>> GetById(int id, bool includeData);
    Task<ServiceResponse<CreditPayment>> GetNextPayment(int userId, int creditAccountId);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdatePaymentNumber(int id, int deltaNumber);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
}
