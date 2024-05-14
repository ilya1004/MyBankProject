using MyBank.Domain.Models;

namespace MyBank.Application.Services;

public class CreditRequestsService : ICreditRequestsService
{
    private readonly ICreditRequestsRepository _creditRequestsRepository;
    private readonly ICreditAccountsService _creditAccountsService;

    public CreditRequestsService(ICreditRequestsRepository creditRequestsRepository, ICreditAccountsService creditAccountsService)
    {
        _creditRequestsRepository = creditRequestsRepository;
        _creditAccountsService = creditAccountsService;
    }

    public async Task<ServiceResponse<int>> Add(string name, int creditPackageId, int userId)
    {
        var creditRequest = new CreditRequest
        {
            Id = 0,
            Name = name,
            CreationDate = DateTime.UtcNow,
            IsActive = true,
            IsApproved = null,
            CreditPackageId = creditPackageId,
            CreditPackage = null,
            ModeratorId = null,
            Moderator = null,
            UserId = userId,
            User = null
        };

        var id = await _creditRequestsRepository.Add(creditRequest);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = id
        };
    }

    public async Task<ServiceResponse<CreditRequest>> GetById(int id, bool includeData)
    {
        var creditRequest = await _creditRequestsRepository.GetById(id, includeData);

        if (creditRequest == null)
        {
            return new ServiceResponse<CreditRequest>
            {
                Status = false,
                Message = $"Credit request with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<CreditRequest>
        {
            Status = true,
            Message = "Success",
            Data = creditRequest
        };
    }

    public async Task<ServiceResponse<List<CreditRequest>>> GetAllByUser(int userId)
    {
        var list = await _creditRequestsRepository.GetAllByUser(userId);

        return new ServiceResponse<List<CreditRequest>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<List<CreditRequest>>> GetAllInfo(bool includeData, bool? isAnswered, bool? isApproved)
    {
        var list = await _creditRequestsRepository.GetAll(includeData, isAnswered, isApproved);

        return new ServiceResponse<List<CreditRequest>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateIsApproved(int id, int moderatorId, bool isApproved)
    {
        var status = await _creditRequestsRepository.UpdateIsApproved(id, moderatorId, isApproved);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit request with given id ({id}) not found",
                Data = default
            };
        }

        if (isApproved)
        {
            var creditRequest = await _creditRequestsRepository.GetById(id, true);

            var creditAccount = new CreditAccount
            {
                Id = 0,
                Name = creditRequest.Name,
                Number = "",
                CurrentBalance = creditRequest!.CreditPackage!.CreditStartBalance,
                CreditStartBalance = creditRequest.CreditPackage.CreditStartBalance,
                CreditGrantedAmount = creditRequest.CreditPackage.CreditGrantedAmount,
                CreationDate = DateTime.UtcNow,
                ClosingDate = null,
                IsActive = true,
                InterestRate = creditRequest.CreditPackage.InterestRate,
                InterestCalculationType = creditRequest.CreditPackage.InterestCalculationType,
                CreditTermInDays = creditRequest.CreditPackage.CreditTermInDays,
                TotalPaymentsNumber = creditRequest.CreditPackage.CreditTermInDays / 30,
                MadePaymentsNumber = 0,
                HasPrepaymentOption = creditRequest.CreditPackage.HasPrepaymentOption,
                UserId = creditRequest.UserId,
                CurrencyId = creditRequest.CreditPackage.CurrencyId,
                ModeratorApprovedId = moderatorId,
            };

            await _creditAccountsService.Add(creditAccount);
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
        var status = await _creditRequestsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe credit request with given id ({id}) not found",
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
