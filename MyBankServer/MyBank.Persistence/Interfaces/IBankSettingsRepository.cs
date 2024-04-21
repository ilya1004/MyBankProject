namespace MyBank.Persistence.Interfaces;

public interface IBankSettingsRepository
{
    Task<BankSettings> GetById(int id);
    Task<bool> Update(int id, decimal creditInterestRate, decimal depositInterestRate);
}
