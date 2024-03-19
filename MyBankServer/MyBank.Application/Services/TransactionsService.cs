using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;

namespace MyBank.Application.Services;


public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;
    public TransactionsService(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<ServiceResponse<int>> Add(Transaction transaction, int personalAccountId)
    {
        var id = await _transactionsRepository.Add(transaction, personalAccountId);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountId(int personalAccountId)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountId(personalAccountId);

        return new ServiceResponse<List<Transaction>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountDate(personalAccountId, dateTimeStart, dateTimeEnd);

        return new ServiceResponse<List<Transaction>> { Status = true, Message = "Success", Data = list };
    }
}
