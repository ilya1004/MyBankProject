namespace MyBank.Application.Services;

public class CardsService : ICardsService
{
    private readonly ICardsRepository _cardsRepository;
    private readonly ICardNumberProvider _cardNumberProvider;

    public CardsService(ICardsRepository cardsRepository, ICardNumberProvider cardNumberProvider)
    {
        _cardsRepository = cardsRepository;
        _cardNumberProvider = cardNumberProvider;
    }

    public async Task<ServiceResponse<int>> Add(int userId, string name, string pincode, int durationInYears, int cardPackageId, int personalAccountId)
    {
        var cardNumber = _cardNumberProvider.GenerateCardNumber(16);
        var cvvCode = _cardNumberProvider.GenerateCardCvv(3);

        var card = new Card {
            Id = 0,
            Name = name,
            Number = cardNumber,
            CreationDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddYears(durationInYears),
            CvvCode = cvvCode,
            Pincode = pincode,
            IsOperationsAllowed = true,
            IsActive = true,
            CardPackageId = cardPackageId,
            UserId = userId,
            PersonalAccountId = personalAccountId
        };

        var id = await _cardsRepository.Add(card);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<Card>> GetById(int id, bool includeData)
    {
        var card = await _cardsRepository.GetById(id, includeData);

        if (card == null)
        {
            return new ServiceResponse<Card>
            {
                Status = false,
                Message = $"Card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Card>
        {
            Status = true,
            Message = "Success",
            Data = card
        };
    }

    public async Task<ServiceResponse<Card>> GetByNumber(string number, bool includeData)
    {
        var card = await _cardsRepository.GetByNumber(number, true, true);

        if (card == null)
        {
            return new ServiceResponse<Card>
            {
                Status = false,
                Message = $"Card with given number ({number}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Card>
        {
            Status = true,
            Message = "Success",
            Data = card
        };
    }

    public async Task<ServiceResponse<List<Card>>> GetAllByUser(int userId, bool includeData)
    {
        var list = await _cardsRepository.GetAllByUser(userId, includeData);

        return new ServiceResponse<List<Card>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdatePincode(int id, string pincode)
    {
        var status = await _cardsRepository.UpdatePincode(id, pincode);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateName(int id, string name)
    {
        var status = await _cardsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateOperationsStatus(int id, bool cardOpersStatus)
    {
        var status = await _cardsRepository.UpdateOpersStatus(id, cardOpersStatus);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool cardStatus)
    {
        var status = await _cardsRepository.UpdateStatus(id, cardStatus);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _cardsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe card with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
