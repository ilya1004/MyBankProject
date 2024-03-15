using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class CreditRequestsService : ICreditRequestsService
{
    private readonly ICreditRequestsRepository _creditRequestsRepository;
    public CreditRequestsService(ICreditRequestsRepository creditRequestsRepository)
    {
        _creditRequestsRepository = creditRequestsRepository;
    }

    public async Task<int> Add(CreditRequest creditRequest, int moderatorId, int userId)
    {
        return await _creditRequestsRepository.Add(creditRequest, moderatorId, userId);
    }

    public async Task<CreditRequest> GetById(int id)
    {
        return await _creditRequestsRepository.GetById(id);
    }

    public async Task<List<CreditRequest>> GetAllByUser(int userId)
    {
        return await _creditRequestsRepository.GetAllByUser(userId);
    }

    public async Task<bool> UpdateIsApproved(int id, bool isApproved)
    {
        return await _creditRequestsRepository.UpdateIsApproved(id, isApproved);
    }

    public async Task<bool> Delete(int id)
    {
        return await _creditRequestsRepository.Delete(id);
    }
}
