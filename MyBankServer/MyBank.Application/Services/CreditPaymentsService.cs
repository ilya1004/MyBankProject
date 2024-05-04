namespace MyBank.Application.Services;

public class CreditPaymentsService : ICreditPaymentsService
{
    private readonly ICreditPaymentsRepository _creditPaymentsRepository;
    private readonly ICreditAccountsRepository _creditAccountsRepository;
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public CreditPaymentsService(ICreditPaymentsRepository creditPaymentsRepository, ICreditAccountsRepository creditAccountsRepository, IPersonalAccountsRepository personalAccountsRepository, ITransactionsRepository transactionsRepository)
    {
        _creditPaymentsRepository = creditPaymentsRepository;
        _creditAccountsRepository = creditAccountsRepository;
        _personalAccountsRepository = personalAccountsRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<ServiceResponse<int>> Add(decimal paymentAmount, int paymentNumber, int creditAccountId, string creditAccountNumber, int personalAccountId, string personalAccountNumber, string userNickname, int userId)
    {
        var creditPayment = new CreditPayment
        {
            Id = 0,
            PaymentAmount = paymentAmount,
            PaymentNumber = paymentNumber,
            Datetime = DateTime.UtcNow,
            Status = true,
            CreditAccountId = creditAccountId,
            CreditAccount = null,
            UserId = userId,
            User = null,
        };

        var id = await _creditPaymentsRepository.Add(creditPayment);

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, -paymentAmount);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };
        }

        var transaction = new Transaction
        {
            Id = 0,
            PaymentAmount = paymentAmount,
            Datetime = DateTime.UtcNow,
            Status = true,
            Information = "Платеж по кредиту",
            AccountSenderNumber = personalAccountNumber,
            UserSenderNickname = userNickname,
            AccountRecipientNumber = creditAccountNumber,
            UserRecipientNickname = userNickname
        };

        id = await _transactionsRepository.Add(transaction);

        status = await _creditAccountsRepository.UpdatePaymentNumber(creditAccountId, 1);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({creditAccountId}) not found",
                Data = default
            };
        }

        status = await _creditAccountsRepository.UpdateBalanceDelta(creditAccountId, -paymentAmount);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({creditAccountId}) not found",
                Data = default
            };
        }

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

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool paymentStatus)
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
