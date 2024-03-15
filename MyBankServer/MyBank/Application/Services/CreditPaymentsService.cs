using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class CreditPaymentsService : ICreditPaymentsService
{
    private readonly ICreditPaymentsRepository _creditPaymentsRepository;
    public CreditPaymentsService(ICreditPaymentsRepository creditPaymentsRepository)
    {
        _creditPaymentsRepository = creditPaymentsRepository;
    }

    public async Task<int> Add(CreditPayment creditPayment, int creditAccountId, int userId)
    {
        return await _creditPaymentsRepository.Add(creditPayment, creditAccountId, userId);
    }

    public async Task<CreditPayment> GetById(int id)
    {
        return await _creditPaymentsRepository.GetById(id);
    }

    public async Task<List<CreditPayment>> GetAllByUserCredit(int userId, int creditAccountId)
    {
        return await _creditPaymentsRepository.GetAllByUserCredit(userId, creditAccountId);
    }

    public async Task<bool> UpdateStatus(int id, string status)
    {
        return await _creditPaymentsRepository.UpdateStatus(id, status);
    }

    public async Task<bool> Delete(int id)
    {
        return await _creditPaymentsRepository.Delete(id);
    }
}
