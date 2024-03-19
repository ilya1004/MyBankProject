using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface IPersonalAccountsService
{
    Task<ServiceResponse<int>> Add(PersonalAccount personalAccount, int userId, int currencyId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId);
    Task<ServiceResponse<PersonalAccount>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname);
}