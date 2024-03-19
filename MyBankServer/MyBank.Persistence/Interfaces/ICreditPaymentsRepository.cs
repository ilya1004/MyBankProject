using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface ICreditPaymentsRepository
{
    Task<int> Add(CreditPayment creditPayment, int creditAccountId, int userId);

    Task<CreditPayment> GetById(int id);

    Task<List<CreditPayment>> GetAllByCredit(int creditAccountId);

    Task<bool> UpdateStatus(int id, string status);

    Task<bool> Delete(int id);
}