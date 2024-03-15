using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ICreditPaymentsService
    {
        Task<int> Add(CreditPayment creditPayment, int creditAccountId, int userId);
        Task<bool> Delete(int id);
        Task<List<CreditPayment>> GetAllByUserCredit(int userId, int creditAccountId);
        Task<CreditPayment> GetById(int id);
        Task<bool> UpdateStatus(int id, string status);
    }
}