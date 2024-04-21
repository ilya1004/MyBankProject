
namespace MyBank.Application.Services;

public class BankSettingsService : IBankSettingsService
{
    private readonly IBankSettingsRepository _bankSettingsRepository;
    public BankSettingsService(IBankSettingsRepository bankSettingsRepository)
    {
        _bankSettingsRepository = bankSettingsRepository;
    }

    public async Task<ServiceResponse<BankSettings>> GetById(int id)
    {
        var bankSettings = await _bankSettingsRepository.GetById(id);

        if (bankSettings == null)
        {
            return new ServiceResponse<BankSettings>
            {
                Status = false,
                Message = "Произошла неизвестная ошибка при получении данных",
                Data = default
            };
        }

        return new ServiceResponse<BankSettings>
        {
            Status = true,
            Message = "Success",
            Data = bankSettings
        };
    }

    public async Task<ServiceResponse<bool>> Update(int id, decimal creditInterestRate, decimal depositInterestRate)
    {
        var status = await _bankSettingsRepository.Update(id, creditInterestRate, depositInterestRate);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Произошла неизвестная ошибка при обновлении данных",
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
