namespace MyBank.Application.Interfaces;

public interface ITransactionsService
{
    Task<ServiceResponse<int>> Add(Transaction transaction);
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(
        string personalAccountNumber,
        DateOnly dateStart,
        DateOnly dateEnd
    );
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(
        string personalAccountNumber
    );
}
