namespace MyBank.Persistence.Interfaces;

public interface ICreditRequestsRepository
{
    Task<int> Add(CreditRequest creditRequest);
    Task<CreditRequest> GetById(int id, bool includeData);
    Task<List<CreditRequest>> GetAllByUser(int userId);
    Task<List<CreditRequest>> GetAll(bool includeData, bool? isAnswered, bool? isApproved);
    Task<bool> UpdateIsApproved(int id, int moderatorId, bool IsApproved);
    Task<bool> Delete(int id);
}
