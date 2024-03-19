using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;
using System.Xml.Linq;

namespace MyBank.Application.Services;


public class CardsService : ICardsService
{
    private readonly ICardsRepository _cardsRepository;
    public CardsService(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<ServiceResponse<int>> Add(Card card, int cardPackageId, int userId, int personalAccountId)
    {
        var id = await _cardsRepository.Add(card, cardPackageId, userId, personalAccountId);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<Card>> GetById(int id)
    {
        var card = await _cardsRepository.GetById(id);

        if (card == null)
        {
            return new ServiceResponse<Card> { Status = false, Message = $"Card with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<Card> { Status = true, Message = "Success", Data = card };
    }

    public async Task<ServiceResponse<Card>> GetByNumber(string number)
    {
        var card = await _cardsRepository.GetByNumber(number);

        if (card == null)
        {
            return new ServiceResponse<Card> { Status = false, Message = $"Card with given number ({number}) not found", Data = default };
        }

        return new ServiceResponse<Card> { Status = true, Message = "Success", Data = card };
    }

    public async Task<ServiceResponse<List<Card>>> GetAllByUser(int userId)
    {
        var list = await _cardsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<Card>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdatePincode(int id, string pincode)
    {
        var status = await _cardsRepository.UpdatePincode(id, pincode);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateName(int id, string name)
    {
        var status = await _cardsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool cardStatus)
    {
        var status = await _cardsRepository.UpdateStatus(id, cardStatus);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _cardsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
