namespace MyBank.Application.Interfaces;

public interface ICardsService
{
    Task<ServiceResponse<int>> Add(int userId, string name, string pincode, int durationInYears, int cardPackageId, int personalAccountId);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<Card>>> GetAllByUser(int userId, bool includeData);
    Task<ServiceResponse<Card>> GetById(int id, bool includeData);
    Task<ServiceResponse<Card>> GetByNumber(string number, bool includeData);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdatePincode(int id, string pincode);
    Task<ServiceResponse<bool>> UpdateOperationsStatus(int id, bool cardStatus);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
}
