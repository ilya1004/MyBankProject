using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;

namespace MyBank.Application.Services;

public class CreditAccountsService : ICreditAccountsService
{
    private readonly ICreditAccountsRepository _creditAccountsRepository;
    public CreditAccountsService(ICreditAccountsRepository creditAccountsRepository)
    {
        _creditAccountsRepository = creditAccountsRepository;
    }

    public async Task<int> Add(CreditAccount creditAccount, int userId, int currencyId, int moderatorId)
    {
        return await _creditAccountsRepository.Add(creditAccount, userId, currencyId, moderatorId);
    }

    public async Task<CreditAccount> GetById(int id)
    {
        return await _creditAccountsRepository.GetById(id);
    }

    public async Task<List<CreditAccount>> GetAllByUser(int userId)
    {
        return await _creditAccountsRepository.GetAllByUser(userId);
    }

    public async Task<bool> UpdateInfo(int id, string name, bool isActive)
    {
        return await _creditAccountsRepository.UpdateInfo(id, name, isActive);
    }

    public async Task<bool> UpdateBalance(int id, decimal deltaNumber)
    {
        return await _creditAccountsRepository.UpdateBalance(id, deltaNumber);
    }

    public async Task<bool> UpdatePaymentNumber(int id, int deltaNumber)
    {
        return await _creditAccountsRepository.UpdatePaymentNumber(id, deltaNumber);
    }

    public async Task<bool> Delete(int id)
    {
        return await _creditAccountsRepository.Delete(id);
    }
}
