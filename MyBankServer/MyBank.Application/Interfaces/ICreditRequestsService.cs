namespace MyBank.Application.Interfaces;

public interface ICreditRequestsService
{
    Task<ServiceResponse<int>> Add(string name, int creditPackageId, int userId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditRequest>>> GetAllByUser(int userId);
    Task<ServiceResponse<List<CreditRequest>>> GetAllInfo(bool includeData, bool? isAnswered, bool? isApproved);
    Task<ServiceResponse<CreditRequest>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateIsApproved(int id, int moderatorId, bool isApproved);
}
