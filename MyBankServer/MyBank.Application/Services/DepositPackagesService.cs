namespace MyBank.Application.Services;

public class DepositPackagesService : IDepositPackagesService
{
    private readonly IDepositPackagesRepository _depositPackagesRepository;

    public DepositPackagesService(IDepositPackagesRepository depositPackagesRepository)
    {
        _depositPackagesRepository = depositPackagesRepository;
    }

    public async Task<ServiceResponse<int>> Add(string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays,
        bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId)
    {
        var depositPackage = new DepositPackage
        {
            Id = 0,
            Name = name,
            DepositStartBalance = depositStartBalance,
            InterestRate = interestRate,
            DepositTermInDays = depositTermInDays,
            IsRevocable = isRevocable,
            HasCapitalisation = hasCapitalisation,
            HasInterestWithdrawalOption = hasInterestWithdrawalOption,
            IsActive = true,
            CurrencyId = currencyId
        };

        var id = await _depositPackagesRepository.Add(depositPackage);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<DepositPackage>> GetById(int id, bool includeData)
    {
        var depositPackage = await _depositPackagesRepository.GetById(id, includeData);

        if (depositPackage == null)
        {
            return new ServiceResponse<DepositPackage>
            {
                Status = false,
                Message = $"Deposit package with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<DepositPackage>
        {
            Status = true,
            Message = "Success",
            Data = depositPackage
        };
    }

    public async Task<ServiceResponse<List<DepositPackage>>> GetAll(bool includeData)
    {
        var list = await _depositPackagesRepository.GetAll(includeData);

        return new ServiceResponse<List<DepositPackage>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId)
    {
        var status = await _depositPackagesRepository.UpdateInfo(id, name, depositStartBalance, interestRate, depositTermInDays, isRevocable, hasCapitalisation, hasInterestWithdrawalOption, currencyId);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit package with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive)
    {
        var status = await _depositPackagesRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit package with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _depositPackagesRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit package with given id ({id}) not found",
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
