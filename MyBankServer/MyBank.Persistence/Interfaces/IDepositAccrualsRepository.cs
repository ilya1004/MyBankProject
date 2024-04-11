namespace MyBank.Persistence.Interfaces;

public interface IDepositAccrualsRepository
{
    Task<int> Add(DepositAccrual depositAccrual);

    Task<DepositAccrual> GetById(int id);

    Task<List<DepositAccrual>> GetAllByDepositId(int depositId);
}
