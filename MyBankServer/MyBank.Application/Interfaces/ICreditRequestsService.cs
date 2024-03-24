namespace MyBank.Application.Interfaces;

public interface ICreditRequestsService
{
    Task<ServiceResponse<int>> Add(CreditRequest creditRequest);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditRequest>>> GetAllByUser(int userId);
    Task<ServiceResponse<CreditRequest>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateIsApproved(int id, int moderatorId, bool isApproved);
}
