using MyBank.Domain.Models;
using System.Xml.Linq;

namespace MyBank.Application.Services;

public class CreditAccountsService : ICreditAccountsService
{
    private readonly ICreditAccountsRepository _creditAccountsRepository;
    private readonly IAccNumberProvider _accNumberProvider;

    public CreditAccountsService(ICreditAccountsRepository creditAccountsRepository, IAccNumberProvider accNumberProvider)
    {
        _creditAccountsRepository = creditAccountsRepository;
        _accNumberProvider = accNumberProvider;
    }

    public async Task<ServiceResponse<int>> Add(CreditAccount creditAcc)
    {
        var credit = new CreditAccount
        {
            Id = 0,
            Name = creditAcc.Name,
            Number = "",
            CurrentBalance = creditAcc.CurrentBalance,
            CreditStartBalance = creditAcc.CreditStartBalance,
            CreditGrantedAmount = creditAcc.CreditGrantedAmount,
            CreationDate = DateTime.UtcNow,
            ClosingDate = null,
            IsActive = true,
            InterestRate = creditAcc.InterestRate,
            InterestCalculationType = creditAcc.InterestCalculationType,
            CreditTermInDays = creditAcc.CreditTermInDays,
            TotalPaymentsNumber = creditAcc.CreditTermInDays / 30,
            MadePaymentsNumber = 0,
            HasPrepaymentOption = creditAcc.HasPrepaymentOption,
            UserId = creditAcc.UserId,
            User = null,
            CurrencyId = creditAcc.CurrencyId,
            Currency = null,
            ModeratorApprovedId = creditAcc.ModeratorApprovedId,
            ModeratorApproved = null,
            Payments = [],
            Transactions = [],
        };

        //if (creditAcc.InterestCalculationType == "differential")
        //{
        //    var paymentBody = credit.CreditGrantedAmount / (credit.CreditTermInDays / 30);
        //    var paymentPercent = credit.CurrentBalance * credit.InterestRate / 12;

        //    payment.PaymentAmount = paymentBody + paymentPercent;

            
        //}
        //else if (creditAcc.InterestCalculationType == "annuity")
        //{
        //    var paymentAmount = credit.CreditStartBalance / (credit.CreditTermInDays / 30);

        //    var totalPaymentAmount = paymentAmount * credit.TotalPaymentsNumber;
        //    credit.CreditStartBalance = totalPaymentAmount;
        //}

        var id = await _creditAccountsRepository.Add(credit);

        string accNumber = _accNumberProvider.GenerateIBAN(id);

        var status = await _creditAccountsRepository.SetAccountNumber(id, accNumber);

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

    public async Task<ServiceResponse<CreditAccount>> GetById(int id, bool includeData)
    {
        var creditAccount = await _creditAccountsRepository.GetById(id, includeData);

        if (creditAccount == null)
        {
            return new ServiceResponse<CreditAccount>
            {
                Status = false,
                Message = $"Credit account with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<CreditAccount>
        {
            Status = true,
            Message = "Success",
            Data = creditAccount
        };
    }

    public async Task<ServiceResponse<CreditPayment>> GetNextPayment(int userId, int creditAccountId)
    {
        var credit = await _creditAccountsRepository.GetById(creditAccountId, false);

        var payment = new CreditPayment
        {
            Id = 0,
            PaymentAmount = 0,
            PaymentNumber = credit.MadePaymentsNumber + 1,
            Datetime = DateTime.UtcNow,
            Status = false,
            CreditAccountId = credit.Id,
            CreditAccount = null,
            UserId = userId,
            User = null,
        };

        if (credit.InterestCalculationType == "differential")
        {
            var paymentBody = credit.CreditGrantedAmount / (credit.CreditTermInDays / 30);
            var paymentPercent = credit.CurrentBalance * credit.InterestRate / (12 * 100);
            
            payment.PaymentAmount = paymentBody + paymentPercent;
        }
        else if (credit.InterestCalculationType == "annuity")
        {
            payment.PaymentAmount = credit.CreditStartBalance / (credit.CreditTermInDays / 30);
        }

        payment.Datetime = credit.CreationDate.AddMonths((DateTime.UtcNow - credit.CreationDate).Days / 30 - credit.MadePaymentsNumber + 1);

        return new ServiceResponse<CreditPayment>
        {
            Status = true,
            Message = "Success",
            Data = payment
        };
    }

    public async Task<ServiceResponse<List<CreditAccount>>> GetAllByUser(int userId, bool includeData, bool onlyActive)
    {
        var list = await _creditAccountsRepository.GetAllByUser(userId, includeData, onlyActive);

        return new ServiceResponse<List<CreditAccount>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateName(int id, string name)
    {
        var status = await _creditAccountsRepository.UpdateName(id, name);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdateBalance(int id, decimal deltaNumber)
    {
        var status = await _creditAccountsRepository.UpdateBalanceDelta(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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

    public async Task<ServiceResponse<bool>> UpdatePaymentNumber(int id, int deltaNumber)
    {
        var status = await _creditAccountsRepository.UpdatePaymentNumber(id, deltaNumber);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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
        var status = await _creditAccountsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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
        var status = await _creditAccountsRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit account with given id ({id}) not found",
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
