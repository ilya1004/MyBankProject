namespace MyBank.Application.Services;

public class CreditAccountsService : ICreditAccountsService
{
    private readonly ICreditAccountsRepository _creditAccountsRepository;

    public CreditAccountsService(ICreditAccountsRepository creditAccountsRepository)
    {
        _creditAccountsRepository = creditAccountsRepository;
    }

    public async Task<ServiceResponse<int>> Add(CreditAccount creditAccount)
    {
        var id = await _creditAccountsRepository.Add(creditAccount);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<CreditAccount>> GetById(int id, bool includeData)
    {
        var card = await _creditAccountsRepository.GetById(id, includeData);

        if (card == null)
        {
            return new ServiceResponse<CreditAccount>
            {
                Status = false,
                Message = $"Credit account with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<CreditAccount>
        {
            Status = true,
            Message = "Success",
            Data = card
        };
    }

    public async Task<ServiceResponse<List<CreditAccount>>> GetAllByUser(int userId, bool includeData)
    {
        var list = await _creditAccountsRepository.GetAllByUser(userId, includeData);

        return new ServiceResponse<List<CreditAccount>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, bool isActive)
    {
        var status = await _creditAccountsRepository.UpdateInfo(id, name, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber)
    {
        var status = await _creditAccountsRepository.UpdateBalance(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdatePaymentNumber(int id, int deltaNumber)
    {
        var status = await _creditAccountsRepository.UpdatePaymentNumber(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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
        var status = await _creditAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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
