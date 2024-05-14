namespace MyBank.Application.Services;

public class CreditPackagesService : ICreditPackagesService
{
    private readonly ICreditPackagesRepository _creditPackagesRepository;

    public CreditPackagesService(ICreditPackagesRepository creditPackagesRepository)
    {
        _creditPackagesRepository = creditPackagesRepository;
    }

    public async Task<ServiceResponse<int>> Add(CreditPackage creditPackage)
    {
        var id = await _creditPackagesRepository.Add(creditPackage);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<CreditPackage>> GetById(int id, bool includeData)
    {
        var creditPackage = await _creditPackagesRepository.GetById(id, includeData);

        if (creditPackage == null)
        {
            return new ServiceResponse<CreditPackage>
            {
                Status = false,
                Message = $"Credit package with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<CreditPackage>
        {
            Status = true,
            Message = "Success",
            Data = creditPackage
        };
    }

    public async Task<ServiceResponse<List<CreditPackage>>> GetAll(bool includeData, bool onlyActive)
    {
        var list = await _creditPackagesRepository.GetAll(includeData, onlyActive);

        return new ServiceResponse<List<CreditPackage>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal creditStartBalance, decimal creditGrantedAmount, decimal interestRate,
        string interestCalculationType, int creditTermInDays, bool hasPrepaymentOption, int currencyId)
    {
        var status = await _creditPackagesRepository.UpdateInfo(id, name, creditStartBalance, creditGrantedAmount, interestRate,
            interestCalculationType, creditTermInDays, hasPrepaymentOption, currencyId);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit package with given id ({id}) not found",
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
        var status = await _creditPackagesRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit package with given id ({id}) not found",
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
        var status = await _creditPackagesRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit package with given id ({id}) not found",
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
