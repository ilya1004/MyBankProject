namespace MyBank.Persistence.Interfaces;

public interface ICurrenciesRepository
{
    Task<int> Add(Currency currency);

    Task<Currency> GetById(int id);

    Task<Currency> GetByCode(string code);

    Task<List<Currency>> GetAll();

    Task<bool> UpdateRate(int id, DateTime lastDateRateUpdate, decimal officialRate);

    Task<bool> Delete(int id);
}
