namespace MyBank.Application.Services;

public class DepositAccountsService : IDepositAccountsService
{
    private readonly IDepositAccountsRepository _depositAccountsRepository;
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IAccNumberProvider _accNumberProvider;

    public DepositAccountsService(IDepositAccountsRepository depositAccountsRepository, IPersonalAccountsRepository personalAccountsRepository, ITransactionsRepository transactionsRepository, IAccNumberProvider accNumberProvider)
    {
        _depositAccountsRepository = depositAccountsRepository;
        _personalAccountsRepository = personalAccountsRepository;
        _transactionsRepository = transactionsRepository;
        _accNumberProvider = accNumberProvider;
    }

    public async Task<ServiceResponse<int>> Add(int userId, string name, decimal depositStartBalance, decimal interestRate, int depositTermInDays, bool isRevocable, bool hasCapitalisation, bool hasInterestWithdrawalOption, int currencyId, int personalAccountId)
    {
        var depositAccount = new DepositAccount
        {
            Id = 0,
            Name = name,
            Number = "",
            CurrentBalance = depositStartBalance,
            DepositStartBalance = depositStartBalance,
            CreationDate = DateTime.UtcNow,
            ClosingDate = null,
            IsActive = true,
            InterestRate = interestRate,
            DepositTermInDays = depositTermInDays,
            TotalAccrualsNumber = depositTermInDays / 30,
            MadeAccrualsNumber = 0,
            IsRevocable = isRevocable,
            HasCapitalisation = hasCapitalisation,
            HasInterestWithdrawalOption = hasInterestWithdrawalOption,
            UserId = userId,
            User = null,
            CurrencyId = currencyId,
            Currency = null,
            Accruals = [],
        };

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, -depositStartBalance);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };
        }

        var id = await _depositAccountsRepository.Add(depositAccount);

        string accNumber = _accNumberProvider.GenerateIBAN(id);

        status = await _depositAccountsRepository.SetAccountNumber(id, accNumber);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({personalAccountId}) not found",
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

    public async Task<ServiceResponse<DepositAccount>> GetById(int id, bool includeData)
    {
        var depositAccount = await _depositAccountsRepository.GetById(id, includeData);

        if (depositAccount == null)
        {
            return new ServiceResponse<DepositAccount>
            {
                Status = false,
                Message = $"Deposit account with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<DepositAccount>
        {
            Status = true,
            Message = "Success",
            Data = depositAccount
        };
    }

    public async Task<ServiceResponse<List<DepositAccount>>> GetAllByUser(int userId, bool includeData, bool onlyActive)
    {
        var list = await _depositAccountsRepository.GetAllByUser(userId, includeData, onlyActive);

        return new ServiceResponse<List<DepositAccount>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateName(int id, string name)
    {
        var status = await _depositAccountsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive)
    {
        var status = await _depositAccountsRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateBalanceDelta(int id, decimal deltaNumber)
    {
        var status = await _depositAccountsRepository.UpdateBalanceDelta(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateBalanceValue(int id, decimal newBalance)
    {
        var status = await _depositAccountsRepository.UpdateBalanceValue(id, newBalance);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({id}) not found",
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
        var status = await _depositAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> RevokeDeposit(int depositAccountId, int personalAccountId)
    {
        var depositAccount = await _depositAccountsRepository.GetById(depositAccountId, true);

        if (depositAccount == null)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message =
                    $"Unknown error. Maybe deposit account with given id ({depositAccountId}) not found",
                Data = default
            };
        }

        var personalAccount = await _personalAccountsRepository.GetById(personalAccountId, false);

        if (personalAccount == null)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message =
                    $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, depositAccount.CurrentBalance);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };
        }

        await _transactionsRepository.Add(
            new Transaction(
                0,
                depositAccount.CurrentBalance,
                DateTime.UtcNow,
                true,
                "Списание средств с депозитного счета",
                depositAccount.Number,
                depositAccount.User!.Nickname,
                personalAccount.Number,
                depositAccount.User.Nickname
            )
        );

        status = await _depositAccountsRepository.UpdateClosingInfo(depositAccountId, 0, DateTime.UtcNow, false);

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> WithdrawInterests(int depositAccountId, int personalAccountId)
    {
        var depositAccount = await _depositAccountsRepository.GetById(depositAccountId, true);

        if (depositAccount == null)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe deposit account with given id ({depositAccountId}) not found",
                Data = default
            };

        var personalAccount = await _personalAccountsRepository.GetById(personalAccountId, false);

        if (personalAccount == null)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };

        decimal value = depositAccount.CurrentBalance - depositAccount.DepositStartBalance;

        var status = await _personalAccountsRepository.UpdateBalanceDelta(personalAccountId, value);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };
        }

        var transaction = new Transaction
        {
            Id = 0,
            PaymentAmount = value,
            Datetime = DateTime.UtcNow,
            Status = true,
            Information = "Списание процентов с депозитного счета",
            AccountSenderNumber = depositAccount.Number,
            UserSenderNickname = depositAccount.User!.Nickname,
            AccountRecipientNumber = personalAccount.Number,
            UserRecipientNickname = depositAccount.User!.Nickname
        };

        await _transactionsRepository.Add(transaction);

        status = await _depositAccountsRepository.UpdateBalanceDelta(depositAccountId, -value);

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
    public async Task<ServiceResponse<bool>> MonthUpdateDepositsBalance()
    {
        var deposits = await _depositAccountsRepository.GetAllByCreationDate(true, true, DateTime.UtcNow);

        foreach (var deposit in deposits)
        {
            if (deposit.HasCapitalisation)
            {
                var monthInterestRate = (deposit.InterestRate / 12) / 100;
                var monthPercents = deposit.CurrentBalance * monthInterestRate;
                var status = await _depositAccountsRepository.UpdateBalanceDelta(deposit.Id, monthPercents);
                if (status == false)
                {
                    return new ServiceResponse<bool>
                    {
                        Status = false,
                        Message = $"Произошла ошибка при ежемесячном обновлении баланса депозитов",
                        Data = default
                    };
                }
            } 
            else
            {
                var monthInterestRate = (deposit.InterestRate / 12) / 100;
                var monthPercents = deposit.DepositStartBalance * monthInterestRate;
                var status = await _depositAccountsRepository.UpdateBalanceDelta(deposit.Id, monthPercents);
                if (status == false)
                {
                    return new ServiceResponse<bool>
                    {
                        Status = false,
                        Message = $"Произошла ошибка при ежемесячном обновлении баланса депозитов",
                        Data = default
                    };
                }
            }
        }
        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = true,
        };
    }
}
