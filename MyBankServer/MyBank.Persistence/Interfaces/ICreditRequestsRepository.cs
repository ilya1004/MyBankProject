namespace MyBank.Persistence.Interfaces;

public interface ICreditRequestsRepository
{
    Task<int> Add(CreditRequest creditRequest);

    Task<CreditRequest> GetById(int id);

    Task<List<CreditRequest>> GetAllByUser(int userId);

    Task<bool> UpdateIsApproved(int id, int moderatorId, bool IsApproved);

    Task<bool> Delete(int id);
}