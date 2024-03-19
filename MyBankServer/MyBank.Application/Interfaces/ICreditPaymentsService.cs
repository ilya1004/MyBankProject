using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface ICreditPaymentsService
{
    Task<ServiceResponse<int>> Add(CreditPayment creditPayment, int creditAccountId, int userId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditPayment>>> GetAllByCredit(int creditAccountId);
    Task<ServiceResponse<CreditPayment>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateStatus(int id, string paymentStatus);
}