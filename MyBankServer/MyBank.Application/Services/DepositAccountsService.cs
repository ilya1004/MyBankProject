using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;

namespace MyBank.Application.Services;


public class DepositAccountsService : IDepositAccountsService
{
    private readonly IDepositAccountsRepository _depositAccountsRepository;
    public DepositAccountsService(IDepositAccountsRepository depositAccountsRepository)
    {
        _depositAccountsRepository = depositAccountsRepository;
    }

    public async Task<ServiceResponse<int>> Add(DepositAccount depositAccount, int userId, int currencyId)
    {
        var id = await _depositAccountsRepository.Add(depositAccount, userId, currencyId);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<DepositAccount>> GetById(int id)
    {
        var depositAccount = await _depositAccountsRepository.GetById(id);

        if (depositAccount == null)
        {
            return new ServiceResponse<DepositAccount> { Status = false, Message = $"Deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<DepositAccount> { Status = true, Message = "Success", Data = depositAccount };
    }

    public async Task<ServiceResponse<List<DepositAccount>>> GetAllByUser(int userId)
    {
        var list = await _depositAccountsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<DepositAccount>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive)
    {
        var status = await _depositAccountsRepository.UpdateInfo(id, name, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber)
    {
        var status = await _depositAccountsRepository.UpdateBalance(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _depositAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
