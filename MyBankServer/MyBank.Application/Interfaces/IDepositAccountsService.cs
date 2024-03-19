using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface IDepositAccountsService
{
    Task<ServiceResponse<int>> Add(DepositAccount depositAccount, int userId, int currencyId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<DepositAccount>>> GetAllByUser(int userId);
    Task<ServiceResponse<DepositAccount>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive);
}