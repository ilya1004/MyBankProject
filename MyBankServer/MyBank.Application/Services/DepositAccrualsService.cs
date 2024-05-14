namespace MyBank.Application.Services;

public class DepositAccrualsService : IDepositAccrualsService
{
    private readonly IDepositAccrualsRepository _depositAccrualsRepository;

    public DepositAccrualsService(IDepositAccrualsRepository depositAccrualsRepository)
    {
        _depositAccrualsRepository = depositAccrualsRepository;
    }

    public async Task<ServiceResponse<int>> Add()
    {
        var depositAccrual = new DepositAccrual
        {
            Id = 0,
            AccrualAmount = 100,
            Datetime = DateTime.UtcNow,
            AccrualNumber = 1,
            Status = true,
            DepositAccountId = 1,
            DepositAccount = null,
        };













        var id = await _depositAccrualsRepository.Add(depositAccrual);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<DepositAccrual>> GetById(int id)
    {
        var card = await _depositAccrualsRepository.GetById(id);

        if (card == null)
        {
            return new ServiceResponse<DepositAccrual>
            {
                Status = false,
                Message = $"Deposit accrual with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<DepositAccrual>
        {
            Status = true,
            Message = "Success",
            Data = card
        };
    }

    public async Task<ServiceResponse<List<DepositAccrual>>> GetAllByDepositId(int depositAccountId)
    {
        var list = await _depositAccrualsRepository.GetAllByDepositId(depositAccountId);

        return new ServiceResponse<List<DepositAccrual>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }
}
