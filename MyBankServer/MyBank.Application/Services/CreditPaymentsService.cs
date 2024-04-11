namespace MyBank.Application.Services;

public class CreditPaymentsService : ICreditPaymentsService
{
    private readonly ICreditPaymentsRepository _creditPaymentsRepository;

    public CreditPaymentsService(ICreditPaymentsRepository creditPaymentsRepository)
    {
        _creditPaymentsRepository = creditPaymentsRepository;
    }

    public async Task<ServiceResponse<int>> Add(CreditPayment creditPayment)
    {
        var id = await _creditPaymentsRepository.Add(creditPayment);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<CreditPayment>> GetById(int id)
    {
        var card = await _creditPaymentsRepository.GetById(id);

        if (card == null)
        {
            return new ServiceResponse<CreditPayment>
            {
                Status = false,
                Message = $"Credit payment with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<CreditPayment>
        {
            Status = true,
            Message = "Success",
            Data = card
        };
    }

    public async Task<ServiceResponse<List<CreditPayment>>> GetAllByCredit(int creditAccountId)
    {
        var list = await _creditPaymentsRepository.GetAllByCredit(creditAccountId);

        return new ServiceResponse<List<CreditPayment>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, string paymentStatus)
    {
        var status = await _creditPaymentsRepository.UpdateStatus(id, paymentStatus);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit payment with given id ({id}) not found",
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
        var status = await _creditPaymentsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit payment with given id ({id}) not found",
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
