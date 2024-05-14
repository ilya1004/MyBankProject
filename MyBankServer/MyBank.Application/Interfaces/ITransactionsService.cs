namespace MyBank.Application.Interfaces;

public interface ITransactionsService
{
    Task<ServiceResponse<bool>> Add(decimal paymentAmount, int currencyId, string information, string transactionType, string accountSenderNumber, 
        string userSenderNickname, string? cardRecipientNumber, string? accountRecipientNumber, string? userRecipientNickname);
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber, DateTime dateStart, DateTime dateEnd);
    Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber);
    Task<ServiceResponse<List<Transaction>>> GetAll();
}
