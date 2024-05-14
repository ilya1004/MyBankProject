using Microsoft.EntityFrameworkCore.Storage;

namespace MyBank.Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrenciesRepository _currenciesRepository;

    public CurrencyService(ICurrenciesRepository currenciesRepository)
    {
        _currenciesRepository = currenciesRepository;
    }

    public async Task<ServiceResponse<int>> Add(Currency currency)
    {

        var id = await _currenciesRepository.Add(currency);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<Currency>> GetById(int id)
    {
        var currency = await _currenciesRepository.GetById(id);

        if (currency == null)
        {
            return new ServiceResponse<Currency>
            {
                Status = false,
                Message = $"Currency with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Currency>
        {
            Status = true,
            Message = "Success",
            Data = currency
        };
    }

    public async Task<ServiceResponse<Currency>> GetByCode(string code)
    {
        var currency = await _currenciesRepository.GetByCode(code);

        if (currency == null)
        {
            return new ServiceResponse<Currency>
            {
                Status = false,
                Message = $"Currency with given code ({code}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Currency>
        {
            Status = true,
            Message = "Success",
            Data = currency
        };
    }

    public async Task<ServiceResponse<List<Currency>>> GetAll()
    {
        var list = await _currenciesRepository.GetAll();

        return new ServiceResponse<List<Currency>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateRate(int id, DateTime lastRateUpdate, decimal officialRate)
    {
        var status = await _currenciesRepository.UpdateRate(id, lastRateUpdate, officialRate);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe currency with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateRates(List<Currency> listCurr)
    {
        var list = new List<string> {"USD", "RUB", "EUR"};
        foreach (var item in listCurr)
        {
            if (list.Contains(item.Code))
            {
                var status = await _currenciesRepository.UpdateRate(item.Code, DateTime.UtcNow, item.OfficialRate);

                if (status == false)
                {
                    return new ServiceResponse<bool>
                    {
                        Status = false,
                        Message = $"Unknown error. Maybe currency with given code ({item.Code}) not found",
                        Data = default
                    };
                }
            }
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = true
        };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _currenciesRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe currency with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
