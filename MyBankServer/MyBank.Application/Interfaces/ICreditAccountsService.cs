using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface ICreditAccountsService
{
    Task<ServiceResponse<int>> Add(CreditAccount creditAccount, int userId, int currencyId, int moderatorId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditAccount>>> GetAllByUser(int userId);
    Task<ServiceResponse<CreditAccount>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive);
    Task<ServiceResponse<bool>> UpdatePaymentNumber(int id, int deltaNumber);
}