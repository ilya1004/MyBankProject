namespace MyBank.Persistence.Interfaces;

public interface ICardsRepository
{
    Task<int> Add(Card card);
    Task<Card> GetById(int id, bool includeData);
    Task<Card> GetByNumber(string number, bool withPersonalAccount, bool withUser);
    Task<List<Card>> GetAllByUser(int userId, bool includeData);
    Task<bool> IsExist(string cardNumber);
    Task<bool> UpdatePincode(int id, string pincode);
    Task<bool> UpdateName(int id, string name);
    Task<bool> UpdateOpersStatus(int id, bool isOperationsAllowed);
    Task<bool> UpdateStatus(int id, bool isActive);
    Task<bool> Delete(int id);
}
