using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.DataTransferObjects.CurrencyDtos;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;

namespace MyBank.Application.Services;

public class CurrencyService : ICurrencyService
{
	private readonly ICurrenciesRepository _currenciesRepository;
	public CurrencyService(ICurrenciesRepository currenciesRepository)
	{
		_currenciesRepository = currenciesRepository;
	}

	public async Task<ServiceResponse<int>> Add(CurrencyDto currency)
	{
		var id = await _currenciesRepository.Add(
			new Currency(0, currency.Code, currency.Name, currency.Scale, currency.LastDateRateUpdate, currency.OfficialRate));

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

	public async Task<ServiceResponse<Currency>> GetById(int id)
	{
		var currency = await _currenciesRepository.GetById(id);

		if (currency == null)
		{
            return new ServiceResponse<Currency> { Status = false, Message = $"Currency with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<Currency> { Status = true, Message = "Success", Data = currency };
    }

	public async Task<ServiceResponse<Currency>> GetByCode(string code)
	{
		var currency = await _currenciesRepository.GetByCode(code);

        if (currency == null)
        {
            return new ServiceResponse<Currency> { Status = false, Message = $"Currency with given code ({code}) not found", Data = default };
        }

        return new ServiceResponse<Currency> { Status = true, Message = "Success", Data = currency };
    }

	public async Task<ServiceResponse<List<Currency>>> GetAll()
	{
		var list = await _currenciesRepository.GetAll();

        return new ServiceResponse<List<Currency>> { Status = true, Message = "Success", Data = list };
    }

	public async Task<ServiceResponse<bool>> UpdateRate(int id, DateTime lastDateRateUpdate, decimal officialRate)
	{
		var status = await _currenciesRepository.UpdateRate(id, lastDateRateUpdate, officialRate);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe currency with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
	}

	public async Task<ServiceResponse<bool>> Delete(int id)
	{
		var status = await _currenciesRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe currency with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
