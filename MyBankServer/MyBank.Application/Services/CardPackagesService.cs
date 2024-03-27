namespace MyBank.Application.Services;


public class CardPackagesService : ICardPackagesService
{
    private readonly ICardPackagesRepository _cardPackagesRepository;
    public CardPackagesService(ICardPackagesRepository cardPackagesRepository)
    {
        _cardPackagesRepository = cardPackagesRepository;
    }

    public async Task<ServiceResponse<int>> Add(CardPackage cardPackage)
    {
        var id = await _cardPackagesRepository.Add(cardPackage);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<CardPackage>> GetById(int id)
    {
        var cardPackage = await _cardPackagesRepository.GetById(id);

        if (cardPackage == null)
        {
            return new ServiceResponse<CardPackage> { Status = false, Message = $"Card package with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<CardPackage> { Status = true, Message = "Success", Data = cardPackage };
    }

    public async Task<ServiceResponse<List<CardPackage>>> GetAll()
    {
        var list = await _cardPackagesRepository.GetAll();

        return new ServiceResponse<List<CardPackage>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll)
    {
        var status = await _cardPackagesRepository.UpdateInfo(id, name, price, operationsNumber, operationsSum, averageAccountBalance, monthPayroll);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card package with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _cardPackagesRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe card package with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
