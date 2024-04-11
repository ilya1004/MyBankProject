namespace MyBank.Application.Services;

public class PersonalAccountsService : IPersonalAccountsService
{
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ICardsRepository _cardsRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public PersonalAccountsService(
        IPersonalAccountsRepository personalAccountsRepository,
        ICardsRepository cardsRepository,
        ITransactionsRepository transactionsRepository
    )
    {
        _personalAccountsRepository = personalAccountsRepository;
        _cardsRepository = cardsRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<ServiceResponse<int>> Add(PersonalAccount personalAccount)
    {
        var id = await _personalAccountsRepository.Add(personalAccount);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<PersonalAccount>> GetById(int id)
    {
        var personalAccount = await _personalAccountsRepository.GetById(id);

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

    public async Task<ServiceResponse<List<PersonalAccount>>> GetAllByUser(int userId)
    {
        var list = await _personalAccountsRepository.GetAllByUser(userId);

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

    public async Task<ServiceResponse<bool>> UpdateTransfersStatus(
        int id,
        bool isForTransfersByNickname
    )
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

        if (personalAccount.CurrentBalance != 0)
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
                    accountRecipient.UserOwner!.Nickname
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
