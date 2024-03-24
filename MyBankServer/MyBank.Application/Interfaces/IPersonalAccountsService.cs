namespace MyBank.Application.Interfaces;

public interface IPersonalAccountsService
{
    Task<ServiceResponse<int>> Add(PersonalAccount personalAccount);
    Task<ServiceResponse<bool>> CloseAccount(int personalAccountId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId);
    Task<ServiceResponse<PersonalAccount>> GetById(int id);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
    Task<ServiceResponse<bool>> UpdateTransfersStatus(int id, bool isForTransfersByNickname);
    Task<ServiceResponse<bool>> MakeTransfer(int personalAccountId, string accountSenderNumber, string userSenderNickname, string? accountRecipientNumber, string? cardRecipientNumber, string? userRecipientNickname, decimal amount);
}