using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface ICreditRequestsRepository
{
    Task<int> Add(CreditRequest creditRequest, int moderatorId, int userId);

    Task<CreditRequest> GetById(int id);

    Task<List<CreditRequest>> GetAllByUser(int userId);

    Task<bool> UpdateIsApproved(int id, bool IsApproved);

    Task<bool> Delete(int id);
}