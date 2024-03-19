using MyBank.Domain.Models;

namespace MyBank.Persistence.Interfaces;

public interface ICardsRepository
{
    Task<int> Add(Card card, int cardPackageId, int userId, int personalAccountId);

    Task<Card> GetById(int id);

    Task<Card> GetByNumber(string number);

    Task<List<Card>> GetAllByUser(int userId);

    Task<bool> UpdatePincode(int id, string pincode);

    Task<bool> UpdateName(int id, string name);

    Task<bool> UpdateStatus(int id, bool isActive);

    Task<bool> Delete(int id);
}