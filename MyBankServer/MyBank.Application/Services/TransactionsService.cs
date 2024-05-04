namespace MyBank.Application.Services;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;

    public TransactionsService(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<ServiceResponse<int>> Add(Transaction transaction)
    {
        var id = await _transactionsRepository.Add(transaction);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountNumber(personalAccountNumber);

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber, DateOnly dateStart, DateOnly dateEnd)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountDate(personalAccountNumber, 
            new DateTime(dateStart, TimeOnly.MinValue, DateTimeKind.Utc),
            new DateTime(dateEnd, TimeOnly.MaxValue, DateTimeKind.Utc));

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAll()
    {
        var list = await _transactionsRepository.GetAll();

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }
}
