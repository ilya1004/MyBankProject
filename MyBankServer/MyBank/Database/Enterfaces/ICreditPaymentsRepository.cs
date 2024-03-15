using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface ICreditPaymentsRepository
{
    Task<int> Add(CreditPayment creditPayment, int creditAccountId, int userId);

    Task<CreditPayment> GetById(int id);

    Task<List<CreditPayment>> GetAllByUserCredit(int userId, int creditAccountId);

    Task<bool> UpdateStatus(int id, string status);

    Task<bool> Delete(int id);
}