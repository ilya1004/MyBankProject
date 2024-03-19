using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface ITransactionsService
{
    Task<ServiceResponse<int>> Add(Transaction transaction, int personalAccountId);
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountDate(int personalAccountId, DateTime dateTimeStart, DateTime dateTimeEnd);
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountId(int personalAccountId);
}