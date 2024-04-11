namespace MyBank.Application.Interfaces;

public interface IDepositAccrualsService
{
    Task<ServiceResponse<int>> Add();
    Task<ServiceResponse<List<DepositAccrual>>> GetAllByDepositId(int depositAccountId);
    Task<ServiceResponse<DepositAccrual>> GetById(int id);
}
