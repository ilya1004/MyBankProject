namespace MyBank.Application.Services;


public class DepositAccountsService : IDepositAccountsService
{
    private readonly IDepositAccountsRepository _depositAccountsRepository;
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public DepositAccountsService(IDepositAccountsRepository depositAccountsRepository, 
        IPersonalAccountsRepository personalAccountsRepository,
        ITransactionsRepository transactionsRepository)
    {
        _depositAccountsRepository = depositAccountsRepository;
        _personalAccountsRepository = personalAccountsRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<ServiceResponse<int>> Add(DepositAccount depositAccount)
    {
        var id = await _depositAccountsRepository.Add(depositAccount);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = id };
    }

    public async Task<ServiceResponse<DepositAccount>> GetById(int id)
    {
        var depositAccount = await _depositAccountsRepository.GetById(id, false);

        if (depositAccount == null)
        {
            return new ServiceResponse<DepositAccount> { Status = false, Message = $"Deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<DepositAccount> { Status = true, Message = "Success", Data = depositAccount };
    }

    public async Task<ServiceResponse<List<DepositAccount>>> GetAllByUser(int userId)
    {
        var list = await _depositAccountsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<DepositAccount>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateName(int id, string name)
    {
        var status = await _depositAccountsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive)
    {
        var status = await _depositAccountsRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateBalanceDelta(int id, decimal deltaNumber)
    {
        var status = await _depositAccountsRepository.UpdateBalanceDelta(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateBalanceValue(int id, decimal newBalance)
    {
        var status = await _depositAccountsRepository.UpdateBalanceValue(id, newBalance);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _depositAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> RevokeDeposit(int depositAccountId, int personalAccountId)
    {
        var depositAccount = await _depositAccountsRepository.GetById(depositAccountId, true);
        
        if (depositAccount == null)
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({depositAccountId}) not found", Data = default };

        var personalAccount = await _personalAccountsRepository.GetById(personalAccountId);

        if (personalAccount == null)
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found", Data = default };

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, depositAccount.CurrentBalance);

        await _transactionsRepository.Add(
            new Transaction(0, depositAccount.CurrentBalance, DateTime.UtcNow, true, "Списание средств с депозитного счета",
            depositAccount.Number, depositAccount.UserOwner!.Nickname, personalAccount.Number, depositAccount.UserOwner.Nickname));

        status = await _depositAccountsRepository.UpdateClosingInfo(depositAccountId, 0, DateTime.UtcNow, false);

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> WithdrawInterests(int depositAccountId, int personalAccountId)
    {
        var depositAccount = await _depositAccountsRepository.GetById(depositAccountId, true);

        if (depositAccount == null)
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe deposit account with given id ({depositAccountId}) not found", Data = default };

        var personalAccount = await _personalAccountsRepository.GetById(personalAccountId);

        if (personalAccount == null)
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found", Data = default };

        decimal value = depositAccount.CurrentBalance - depositAccount.DepositStartBalance;

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, value);

        await _transactionsRepository.Add(
            new Transaction(0, value, DateTime.UtcNow, true, "Списание процентов с депозитного счета",
            depositAccount.Number, depositAccount.UserOwner!.Nickname, personalAccount.Number, depositAccount.UserOwner!.Nickname));

        status = await _depositAccountsRepository.UpdateBalanceDelta(depositAccountId, -value);

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
