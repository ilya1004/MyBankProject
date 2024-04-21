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
        var list = await _transactionsRepository.GetAllByPersonalAccountNumber(
            personalAccountNumber
        );

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber, DateOnly dateStart, DateOnly dateEnd)
    {
        TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
        var list = await _transactionsRepository.GetAllByPersonalAccountDate(
            personalAccountNumber,
            TimeZoneInfo.ConvertTimeToUtc(new DateTime(dateStart, new TimeOnly(0, 0, 0)), localTimeZone),
            TimeZoneInfo.ConvertTimeToUtc(new DateTime(dateEnd, new TimeOnly(23, 59, 59)), localTimeZone)
        );

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }
}
