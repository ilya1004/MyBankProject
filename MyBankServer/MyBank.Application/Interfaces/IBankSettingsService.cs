
namespace MyBank.Application.Interfaces;

public interface IBankSettingsService
{
    Task<ServiceResponse<BankSettings>> GetById(int id);
    Task<ServiceResponse<bool>> Update(int id, decimal creditInterestRate, decimal depositInterestRate);
}
