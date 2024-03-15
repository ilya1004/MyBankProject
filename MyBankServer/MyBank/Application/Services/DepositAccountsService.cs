using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;
using System.Reflection;

namespace MyBank.Application.Services;

public class DepositAccountsService : IDepositAccountsService
{
    private readonly IDepositAccountsRepository _depositAccountsRepository;
    public DepositAccountsService(IDepositAccountsRepository depositAccountsRepository)
    {
        _depositAccountsRepository = depositAccountsRepository;
    }

    public async Task<int> Add(DepositAccount depositAccount, int userId, int currencyId)
    {
        return await _depositAccountsRepository.Add(depositAccount, userId, currencyId);
    }

    public async Task<DepositAccount> GetById(int id)
    {
        return await _depositAccountsRepository.GetById(id);
    }

    public async Task<List<DepositAccount>> GetAllByUser(int userId)
    {
        return await _depositAccountsRepository.GetAllByUser(userId);
    }

    public async Task<bool> UpdateName(int id, string name, bool isActive)
    {
        return await _depositAccountsRepository.UpdateName(id, name, isActive);
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        return await _depositAccountsRepository.UpdateBalance(id, deltaNumber);
    }

    public async Task<bool> Delete(int id)
    {
        return await _depositAccountsRepository.Delete(id);
    }
}
