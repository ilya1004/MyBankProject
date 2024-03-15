using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ICreditRequestsService
    {
        Task<int> Add(CreditRequest creditRequest, int moderatorId, int userId);
        Task<bool> Delete(int id);
        Task<List<CreditRequest>> GetAllByUser(int userId);
        Task<CreditRequest> GetById(int id);
        Task<bool> UpdateIsApproved(int id, bool isApproved);
    }
}