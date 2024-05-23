using MyBank.Domain.Models;

namespace MyBank.Application.Services;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IPersonalAccountsRepository _personalAccountsRepository;
    private readonly ICardsRepository _cardsRepository;
    private readonly IUsersRepository _usersRepository;

    public TransactionsService(ITransactionsRepository transactionsRepository, IPersonalAccountsRepository personalAccountsRepository, ICardsRepository cardsRepository, IUsersRepository usersRepository)
    {
        _transactionsRepository = transactionsRepository;
        _personalAccountsRepository = personalAccountsRepository;
        _cardsRepository = cardsRepository;
        _usersRepository = usersRepository;
    }

    public async Task<ServiceResponse<bool>> Add(decimal paymentAmount, int currencyId, string information, string transactionType, string accountSenderNumber,
        string userSenderNickname, string? cardRecipientNumber, string? accountRecipientNumber, string? userRecipientNickname)
    {
        if ((transactionType == "my-account" || transactionType == "account") && accountRecipientNumber != null)
        {
            var isExist = await _personalAccountsRepository.IsExist(accountRecipientNumber);

            if (isExist == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = $"Лицевой счет с данным номером не существует или не доступен, проверьте правильность введенных данных",
                    Data = default
                };
            }

            var accountRecipient = await _personalAccountsRepository.GetByNumber(accountRecipientNumber, true);

            var transaction = new Transaction
            {
                Id = 0,
                PaymentAmount = paymentAmount,
                Datetime = DateTime.UtcNow,
                Status = true,
                Information = information,
                AccountSenderNumber = accountSenderNumber,
                UserSenderNickname = userSenderNickname,
                AccountRecipientNumber = accountRecipientNumber,
                UserRecipientNickname = accountRecipient.User!.Nickname
            };

            var id = await _transactionsRepository.Add(transaction);

            var status = await _personalAccountsRepository.UpdateBalanceDelta(accountSenderNumber, -paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }

            status = await _personalAccountsRepository.UpdateBalanceDelta(accountRecipientNumber, paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }
        }
        else if (transactionType == "card" && cardRecipientNumber != null)
        {
            var isExist = await _cardsRepository.IsExist(cardRecipientNumber);

            if (isExist == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = $"Карта с данным номером не существует или не доступна, проверьте правильность введенных данных",
                    Data = default
                };
            }

            var cardRecipient = await _cardsRepository.GetByNumber(cardRecipientNumber, true, true);

            var transaction = new Transaction
            {
                Id = 0,
                PaymentAmount = paymentAmount,
                Datetime = DateTime.UtcNow,
                Status = true,
                Information = information,
                AccountSenderNumber = accountSenderNumber,
                UserSenderNickname = userSenderNickname,
                AccountRecipientNumber = cardRecipient.PersonalAccount!.Number,
                UserRecipientNickname = cardRecipient.User!.Nickname,
            };

            var id = await _transactionsRepository.Add(transaction);

            var status = await _personalAccountsRepository.UpdateBalanceDelta(accountSenderNumber, -paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }

            status = await _personalAccountsRepository.UpdateBalanceDelta(cardRecipient.PersonalAccount.Number, paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }
        }
        else if (transactionType == "nickname" && userRecipientNickname != null)
        {
            var isExist = await _usersRepository.IsExistByNickname(userRecipientNickname);

            if (isExist == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = $"Пользователь с данным никнеймом не найден или не доступен, проверьте правильность введенных данных",
                    Data = default
                };
            }

            var userAccounts = await _personalAccountsRepository.GetAllByUser(userRecipientNickname, true);

            var hasMainAccount =  userAccounts.Any(item => item.IsForTransfersByNickname == true);

            if (!hasMainAccount)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = $"Невозможно провести операцию, поскольку у данного пользователя ни один счет не выбран в качестве основного",
                    Data = default
                };
            }

            var accountRecipient = userAccounts.Find(item => item.IsForTransfersByNickname == true);

            var transaction = new Transaction
            {
                Id = 0,
                PaymentAmount = paymentAmount,
                Datetime = DateTime.UtcNow,
                Status = true,
                Information = information,
                AccountSenderNumber = accountSenderNumber,
                UserSenderNickname = userSenderNickname,
                AccountRecipientNumber = accountRecipient!.Number,
                UserRecipientNickname = accountRecipient.User!.Nickname,
            };

            var id = await _transactionsRepository.Add(transaction);

            var status = await _personalAccountsRepository.UpdateBalanceDelta(accountSenderNumber, -paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }

            status = await _personalAccountsRepository.UpdateBalanceDelta(accountRecipient.Number, paymentAmount);

            if (status == false)
            {
                return new ServiceResponse<bool>
                {
                    Status = false,
                    Message = "Неизвестная ошибка при обновлении баланса счета.",
                    Data = default
                };
            }
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = true
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountNumber(personalAccountNumber);

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAllByPersonalAccountNumber(string personalAccountNumber, DateTime dateStart, DateTime dateEnd)
    {
        var list = await _transactionsRepository.GetAllByPersonalAccountDate(personalAccountNumber, dateStart, dateEnd.AddDays(1));

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<Transaction>>> GetAll()
    {
        var list = await _transactionsRepository.GetAll();

        return new ServiceResponse<List<Transaction>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }
}
