namespace MyBank.Application.Interfaces;

public interface IPersonalAccountsService
{
    Task<ServiceResponse<int>> Add(int userId, string name, int currencyId);
    Task<ServiceResponse<bool>> CloseAccount(int personalAccountId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId, bool includeData);
    Task<ServiceResponse<PersonalAccount>> GetById(int id, bool includeData);
    Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
    Task<ServiceResponse<bool>> UpdateTransfersStatus(int id, bool isForTransfersByNickname);
}
