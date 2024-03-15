using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class CardPackagesService : ICardPackagesService
{
    private readonly ICardPackagesRepository _cardPackagesRepository;
    public CardPackagesService(ICardPackagesRepository cardPackagesRepository)
    {
        _cardPackagesRepository = cardPackagesRepository;
    }

    public async Task<int> Add(CardPackage cardPackage)
    {
        return await _cardPackagesRepository.Add(cardPackage);
    }

    public async Task<CardPackage> GetById(int id)
    {
        return await _cardPackagesRepository.GetById(id);
    }

    public async Task<List<CardPackage>> GetAll()
    {
        return await _cardPackagesRepository.GetAll();
    }

    public async Task<bool> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll)
    {
        return await _cardPackagesRepository.UpdateInfo(id, name, price, operationsNumber, operationsSum, averageAccountBalance, monthPayroll);
    }

    public async Task<bool> Delete(int id)
    {
        return await _cardPackagesRepository.Delete(id);
    }
}
