using Microsoft.EntityFrameworkCore;
using MyBank.Core.DataTransferObjects.CurrencyDtos;
using MyBank.Core.Models;

namespace MyBank.Database.Enterfaces;

public interface ICurrenciesRepository
{
    Task<int> Add(CurrencyDto currency);

    Task<Currency> GetById(int id);

    Task<Currency> GetByCode(string code);

    Task<List<Currency>> GetAll();

    Task<bool> UpdateRate(int id, DateTime lastDateRateUpdate, decimal officialRate);

    Task<bool> Delete(int id);
}