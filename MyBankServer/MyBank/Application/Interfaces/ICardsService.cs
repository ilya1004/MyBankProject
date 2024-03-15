using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface ICardsService
    {
        Task<int> Add(Card card, int cardPackageId, int userId, int personalAccountId);
        Task<bool> Delete(int id);
        Task<List<Card>> GetAllByUser(int userId);
        Task<Card> GetById(int id);
        Task<Card> GetByNumber(string number);
        Task<bool> UpdateName(int id, string name);
        Task<bool> UpdatePincode(int id, string pincode);
        Task<bool> UpdateStatus(int id, bool status);
    }
}