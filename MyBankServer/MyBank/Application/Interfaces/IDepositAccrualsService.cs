using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IDepositAccrualsService
    {
        Task<int> Add();
        Task<List<DepositAccrual>> GetAllByDepositId(int depositAccountId);
        Task<DepositAccrual> GetById(int id);
    }
}