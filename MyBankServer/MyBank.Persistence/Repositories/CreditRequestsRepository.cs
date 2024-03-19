using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Entities;

namespace MyBank.Persistence.Repositories;

public class CreditRequestsRepository : ICreditRequestsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreditRequestsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditRequest creditRequest, int moderatorId, int userId)
    {

        var moderatorEntity = await _dbContext.Moderators
            .FirstOrDefaultAsync(m => m.Id == moderatorId);

        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var creditRequestEntity = new CreditRequestEntity
        {
            Id = 0,
            StartBalance = creditRequest.StartBalance,
            InterestRate = creditRequest.InterestRate,
            InterestCalculationType = creditRequest.InterestCalculationType,
            CreditTermInDays = creditRequest.CreditTermInDays,
            TotalPaymentsNumber = creditRequest.TotalPaymentsNumber,
            HasPrepaymentOption = creditRequest.HasPrepaymentOption,
            IsApproved = creditRequest.IsApproved,
            ModeratorId = moderatorId,
            Moderator = moderatorEntity,
            UserId = userId,
            User = userEntity,
        };

        var item = await _dbContext.CreditRequests.AddAsync(creditRequestEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditRequest> GetById(int id)
    {
        var creditRequestEntity = await _dbContext.CreditRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(cr => cr.Id == id);

        return _mapper.Map<CreditRequest>(creditRequestEntity);
    }

    public async Task<List<CreditRequest>> GetAllByUser(int userId)
    {
        var creditRequestEntitiesList = await _dbContext.CreditRequests
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<CreditRequest>>(creditRequestEntitiesList);
    }

    public async Task<bool> UpdateIsApproved(int id, bool IsApproved)
    {
        var number = await _dbContext.CreditRequests
            .Where(cr => cr.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cr => cr.IsApproved, IsApproved));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditRequests
            .Where(cr => cr.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
