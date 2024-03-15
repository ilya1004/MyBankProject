using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;

namespace MyBank.Application.Services;

public class PersonalAccountsService : IPersonalAccountsService
{
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    public PersonalAccountsService(IPersonalAccountsRepository personalAccountsRepository)
    {
        _personalAccountsRepository = personalAccountsRepository;
    }

    public async Task<int> Add(PersonalAccount personalAccount, int userId, int currencyId)
    {
        return await _personalAccountsRepository.Add(personalAccount, userId, currencyId);
    }

    public async Task<PersonalAccount> GetById(int id)
    {
        return await _personalAccountsRepository.GetById(id);
    }

    public async Task<List<PersonalAccount>> GetAllByUser(int userId)
    {
        return await _personalAccountsRepository.GetAllByUser(userId);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive, bool isForTransfersByNickname)
    {
        return await _personalAccountsRepository.UpdateInfo(id, name, isActive, isForTransfersByNickname);
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        return await _personalAccountsRepository.UpdateBalance(id, deltaNumber);
    }

    public async Task<bool> Delete(int id)
    {
        return await _personalAccountsRepository.Delete(id);
    }
}
