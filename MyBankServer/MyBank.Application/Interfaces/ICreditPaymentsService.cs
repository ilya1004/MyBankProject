namespace MyBank.Application.Interfaces;

public interface ICreditPaymentsService
{
    Task<ServiceResponse<int>> Add(decimal paymentAmount, int paymentNumber, int creditAccountId, string creditAccountNumber, int personalAccountId, string personalAccountNumber, string userNickname, int userId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<CreditPayment>>> GetAllByCredit(int creditAccountId);
    Task<ServiceResponse<CreditPayment>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool paymentStatus);
}
