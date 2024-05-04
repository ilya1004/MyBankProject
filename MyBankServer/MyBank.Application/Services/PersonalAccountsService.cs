namespace MyBank.Application.Services;

public class PersonalAccountsService : IPersonalAccountsService
{
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ICardsRepository _cardsRepository;
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IAccNumberProvider _accNumberProvider;

    public PersonalAccountsService(
        IPersonalAccountsRepository personalAccountsRepository,
        ICardsRepository cardsRepository,
        ITransactionsRepository transactionsRepository,
        IAccNumberProvider accNumberProvider
    )
    {
        _personalAccountsRepository = personalAccountsRepository;
        _cardsRepository = cardsRepository;
        _transactionsRepository = transactionsRepository;
        _accNumberProvider = accNumberProvider;
    }

    public async Task<ServiceResponse<int>> Add(int userId, string name, int currencyId)
    {
        var personalAccount = new PersonalAccount
        {
            Id = 0,
            Name = name,
            Number = "",
            CurrentBalance = decimal.Zero,
            CreationDate = DateTime.UtcNow,
            ClosingDate = null,
            IsActive = true,
            IsForTransfersByNickname = false,
            UserId = userId,
            User = null,
            CurrencyId = currencyId,
            Currency = null,
            Cards = []
        };
        var id = await _personalAccountsRepository.Add(personalAccount);

        string accNumber = _accNumberProvider.GenerateIBAN(id);

        var status = await _personalAccountsRepository.SetAccountNumber(id, accNumber);

        if (status == false)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({id}) not found",
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

    public async Task<ServiceResponse<PersonalAccount>> GetById(int id, bool includeData)
    {
        var personalAccount = await _personalAccountsRepository.GetById(id, includeData);

        if (personalAccount == null)
        {
            return new ServiceResponse<PersonalAccount>
            {
                Status = false,
                Message = $"Personal account with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<PersonalAccount>
        {
            Status = true,
            Message = "Success",
            Data = personalAccount
        };
    }

    public async Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId, bool includeData)
    {
        var list = await _personalAccountsRepository.GetAllByUser(userId, includeData);

        return new ServiceResponse<List<PersonalAccount>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber)
    {
        var status = await _personalAccountsRepository.UpdateBalanceDelta(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({id}) not found",
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
        var status = await _personalAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({id}) not found",
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
        var status = await _personalAccountsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({id}) not found",
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
        var status = await _personalAccountsRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe personal account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateTransfersStatus(int id, bool isForTransfersByNickname)
    {
        var status = await _personalAccountsRepository.UpdateTransfersStatus(
            id,
            isForTransfersByNickname
        );

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Personal account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> CloseAccount(int id)
    {
        var personalAccount = await _personalAccountsRepository.GetById(id, true);

        if (personalAccount == null)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Personal account with given id ({id}) not found",
                Data = default
            };

        if (personalAccount.CurrentBalance != decimal.Zero)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Personal account must have zero current balance to delete it",
                Data = default
            };

        await _personalAccountsRepository.UpdateClosingInfo(id, DateTime.UtcNow, false, false);

        var status = false;
        foreach (var card in personalAccount.Cards)
        {
            status = await _cardsRepository.UpdateStatus(card.Id, false);

            if (status == false)
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

    public async Task<ServiceResponse<bool>> UpdateNicknameTransfersState(int id, bool state)
    {
        var status = await _personalAccountsRepository.UpdateTransfersStatus(id, state);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Personal account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> MakeTransfer(
        int personalAccountId,
        string accountSenderNumber,
        string userSenderNickname,
        string? accountRecipientNumber,
        string? cardRecipientNumber,
        string? userRecipientNickname,
        decimal amount
    )
    {
        var status = await _personalAccountsRepository.UpdateBalanceDelta(
            personalAccountId,
            -amount
        );

        if (status == false)
            return new ServiceResponse<bool>
            {
                Status = false,
                Message =
                    $"Unknown error. Maybe personal account with given id ({personalAccountId}) not found",
                Data = default
            };

        if (accountRecipientNumber != null)
        {
            status = await _personalAccountsRepository.UpdateBalanceDelta(
                accountRecipientNumber,
                amount
            );

            if (status == false)
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message =
                        $"Unknown error. Maybe personal account with given number ({accountRecipientNumber}) not found",
                    Data = default
                };

            var accountRecipient = await _personalAccountsRepository.GetByNumber(
                accountRecipientNumber,
                true
            );

            await _transactionsRepository.Add(
                new Transaction(
                    0,
                    amount,
                    DateTime.UtcNow,
                    true,
                    "Перевод средств по номеру счета",
                    accountSenderNumber,
                    userSenderNickname,
                    accountRecipientNumber,
                    accountRecipient.User!.Nickname
                )
            );
        }
        else if (cardRecipientNumber != null)
        {
            var card = await _cardsRepository.GetByNumber(cardRecipientNumber, true, true);

            if (card == null)
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message =
                        $"Unknown error. Maybe card with given number ({cardRecipientNumber}) not found",
                    Data = default
                };

            status = await _personalAccountsRepository.UpdateBalanceDelta(
                card.PersonalAccount!.Number,
                amount
            );

            if (status == false)
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message =
                        $"Unknown error. Maybe personal account with given number ({card.PersonalAccount!.Number}) not found",
                    Data = default
                };

            await _transactionsRepository.Add(
                new Transaction(
                    0,
                    amount,
                    DateTime.UtcNow,
                    true,
                    "Перевод средств по номеру карты",
                    accountSenderNumber,
                    userSenderNickname,
                    card.PersonalAccount!.Number,
                    card.User!.Nickname
                )
            );
        }
        else if (userRecipientNickname != null)
        {
            var personalAccount = await _personalAccountsRepository.GetIsForTransfersByNickname(
                userRecipientNickname
            );

            if (personalAccount == null)
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = $"Неизвестная ошибка.",
                    Data = default
                };

            status = await _personalAccountsRepository.UpdateBalanceDelta(
                personalAccount.Id,
                amount
            );

            if (status == false)
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message =
                        $"Unknown error. Maybe personal account with given id ({personalAccount.Id}) not found",
                    Data = default
                };

            await _transactionsRepository.Add(
                new Transaction(
                    0,
                    amount,
                    DateTime.UtcNow,
                    true,
                    "Перевод средств по номеру карты",
                    accountSenderNumber,
                    userSenderNickname,
                    personalAccount.Number,
                    userRecipientNickname
                )
            );
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
