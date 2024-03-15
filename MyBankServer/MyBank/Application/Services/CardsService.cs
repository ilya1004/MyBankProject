using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class CardsService : ICardsService
{
    private readonly ICardsRepository _cardsRepository;
    public CardsService(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<int> Add(Card card, int cardPackageId, int userId, int personalAccountId)
    {
        return await _cardsRepository.Add(card, cardPackageId, userId, personalAccountId);
    }

    public async Task<Card> GetById(int id)
    {
        return await _cardsRepository.GetById(id);
    }

    public async Task<Card> GetByNumber(string number)
    {
        return await _cardsRepository.GetByNumber(number);
    }

    public async Task<List<Card>> GetAllByUser(int userId)
    {
        return await _cardsRepository.GetAllByUser(userId);
    }

    public async Task<bool> UpdatePincode(int id, string pincode)
    {
        return await _cardsRepository.UpdatePincode(id, pincode);
    }

    public async Task<bool> UpdateName(int id, string name)
    {
        return await _cardsRepository.UpdateName(id, name);
    }

    public async Task<bool> UpdateStatus(int id, bool status)
    {
        return await _cardsRepository.UpdateStatus(id, status);
    }

    public async Task<bool> Delete(int id)
    {
        return await _cardsRepository.Delete(id);
    }
}
