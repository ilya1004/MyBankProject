using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface ICreditRequestsRepository
{
    Task<int> Add(CreditRequest creditRequest, int moderatorId, int userId);

    Task<CreditRequest> GetById(int id);

    Task<List<CreditRequest>> GetAllByUser(int userId);

    Task<bool> UpdateIsApproved(int id, bool IsApproved);

    Task<bool> Delete(int id);
}