﻿using MyBank.Domain.Models;

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

    public async Task<int> Add(CreditRequest creditRequest)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == creditRequest.UserId);

        var creditRequestEntity = _mapper.Map<CreditRequestEntity>(creditRequest);

        creditRequestEntity.User = userEntity;

        var item = await _dbContext.CreditRequests.AddAsync(creditRequestEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditRequest> GetById(int id, bool includeData)
    {
        IQueryable<CreditRequestEntity> query = _dbContext.CreditRequests.AsNoTracking();

        if (includeData)
        {
            query = query.Include(cr => cr.User)
                         .Include(cr => cr.Moderator)
                         .Include(cr => cr.CreditPackage)
                            .ThenInclude(cp => cp!.Currency);
        }

        var creditRequestEntity = await query.FirstOrDefaultAsync(cr => cr.Id == id);

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

    public async Task<List<CreditRequest>> GetAll(bool includeData, bool? isAnswered, bool? isApproved)
    {
        IQueryable<CreditRequestEntity> query = _dbContext.CreditRequests.AsNoTracking();

        if (includeData)
        {
            query = query.Include(cr => cr.User)
                         .Include(cr => cr.Moderator)
                         .Include(cr => cr.CreditPackage)
                            .ThenInclude(cp => cp!.Currency);
        }

        if (isAnswered != null && isAnswered == true)
        {
            query = query.Where(cr => cr.ModeratorId != null);
        }
        else if (isAnswered != null && isAnswered == false)
        {
            query = query.Where(cr => cr.ModeratorId == null);
        }

        if (isApproved != null && isApproved == true)
        {
            query = query.Where(cr => cr.IsApproved == true);
        }
        else if (isApproved != null && isApproved == false)
        {
            query = query.Where(cr => cr.IsApproved == false);
        }

        var creditRequestEntitiesList = await query.ToListAsync();

        return _mapper.Map<List<CreditRequest>>(creditRequestEntitiesList);
    }

    public async Task<bool> UpdateIsApproved(int id, int moderatorId, bool IsApproved)
    {
        var number = await _dbContext.CreditRequests
            .Where(cr => cr.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cr => cr.IsApproved, IsApproved)
                .SetProperty(cr => cr.ModeratorId, moderatorId));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditRequests
            .Where(cr => cr.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
