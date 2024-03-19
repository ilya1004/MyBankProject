using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface ICreditRequestsService
{
    Task<ServiceResponse<int>> Add(CreditRequest creditRequest, int moderatorId, int userId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditRequest>>> GetAllByUser(int userId);
    Task<ServiceResponse<CreditRequest>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateIsApproved(int id, bool isApproved);
}
