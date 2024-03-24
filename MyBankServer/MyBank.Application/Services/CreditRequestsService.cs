namespace MyBank.Application.Services;


public class CreditRequestsService : ICreditRequestsService
{
    private readonly ICreditRequestsRepository _creditRequestsRepository;
    public CreditRequestsService(ICreditRequestsRepository creditRequestsRepository)
    {
        _creditRequestsRepository = creditRequestsRepository;
    }

    public async Task<ServiceResponse<int>> Add(CreditRequest creditRequest)
    {
        var id = await _creditRequestsRepository.Add(creditRequest);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<CreditRequest>> GetById(int id)
    {
        var creditRequest = await _creditRequestsRepository.GetById(id);

        if (creditRequest == null)
        {
            return new ServiceResponse<CreditRequest> { Status = false, Message = $"Credit request with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<CreditRequest> { Status = true, Message = "Success", Data = creditRequest };
    }

    public async Task<ServiceResponse<List<CreditRequest>>> GetAllByUser(int userId)
    {
        var list = await _creditRequestsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<CreditRequest>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateIsApproved(int id, int moderatorId, bool isApproved)
    {
        var status = await _creditRequestsRepository.UpdateIsApproved(id, moderatorId, isApproved);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe credit request with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _creditRequestsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe credit request with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
