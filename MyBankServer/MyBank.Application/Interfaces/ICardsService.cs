namespace MyBank.Application.Interfaces;

public interface ICardsService
{
    Task<ServiceResponse<int>> Add(Card card);
    Task<ServiceResponse<bool>> Delete(int id);
    Task<ServiceResponse<List<Card>>> GetAllByUser(int userId, bool includeData);
    Task<ServiceResponse<Card>> GetById(int id);
    Task<ServiceResponse<Card>> GetByNumber(string number);
    Task<ServiceResponse<bool>> UpdateName(int id, string name);
    Task<ServiceResponse<bool>> UpdatePincode(int id, string pincode);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool cardStatus);
}
