namespace MyBank.Application.Interfaces;

public interface ICurrencyService
{
    Task<ServiceResponse<int>> Add(Currency currency);
    Task<ServiceResponse<Currency>> GetById(int id);
    Task<ServiceResponse<Currency>> GetByCode(string code);
    Task<ServiceResponse<List<Currency>>> GetAll();
    Task<ServiceResponse<bool>> UpdateRate(int id, DateTime lastDateRateUpdate, decimal officialRate);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<bool>> UpdateRates(List<Currency> listCurr);
}
