namespace MyBank.Persistence.Interfaces;

public interface ITransactionsRepository
{
    Task<int> Add(Transaction transaction);

    Task<List<Transaction>> GetAllByPersonalAccountNumber(string personalAccountNumber);

    Task<List<Transaction>> GetAllByPersonalAccountDate(string personalAccountNumber, DateTime dateTimeStart, DateTime dateTimeEnd);
}