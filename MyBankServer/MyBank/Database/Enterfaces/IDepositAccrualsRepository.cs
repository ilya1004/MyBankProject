using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Entities;

namespace MyBank.Database.Enterfaces;

public interface IDepositAccrualsRepository
{
    Task<int> Add(DepositAccrual depositAccrual, int depositAccountId);

    Task<DepositAccrual> GetById(int id);

    Task<List<DepositAccrual>> GetAllByDepositId(int depositId);
}