using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;
using System.Xml.Linq;

namespace MyBank.Application.Services;


public class PersonalAccountsService : IPersonalAccountsService
{
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    public PersonalAccountsService(IPersonalAccountsRepository personalAccountsRepository)
    {
        _personalAccountsRepository = personalAccountsRepository;
    }

    public async Task<ServiceResponse<int>> Add(PersonalAccount personalAccount, int userId, int currencyId)
    {
        var id = await _personalAccountsRepository.Add(personalAccount, userId, currencyId);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<PersonalAccount>> GetById(int id)
    {
        var personalAccount = await _personalAccountsRepository.GetById(id);

        if (personalAccount == null)
        {
            return new ServiceResponse<PersonalAccount> { Status = false, Message = $"Personal account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<PersonalAccount> { Status = true, Message = "Success", Data = personalAccount };
    }

    public async Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId)
    {
        var list = await _personalAccountsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<PersonalAccount>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname)
    {
        var status = await _personalAccountsRepository.UpdateInfo(id, name, isActive, isForTransfersByNickname);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe personal account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber)
    {
        var status = await _personalAccountsRepository.UpdateBalance(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe personal account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _personalAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe personal account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
