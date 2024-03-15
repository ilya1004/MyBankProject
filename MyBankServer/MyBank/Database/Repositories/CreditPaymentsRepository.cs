using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class CreditPaymentsRepository : ICreditPaymentsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreditPaymentsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditPayment creditPayment, int creditAccountId, int userId)
    {
        var creditAccountEntity = await _dbContext.CreditAccounts
           .FirstOrDefaultAsync(ca => ca.Id == creditAccountId);

        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var сreditPaymentEntity = new CreditPaymentEntity
        {
            Id = 0,
            PaymentAmount = creditPayment.PaymentAmount,
            PaymentNumber = creditPayment.PaymentNumber,
            Datetime = creditPayment.Datetime,
            Status = creditPayment.Status,
            CreditAccountId = creditAccountId,
            CreditAccount = creditAccountEntity,
            UserId = userId,
            User = userEntity
        };

        var item = await _dbContext.CreditPayments.AddAsync(сreditPaymentEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditPayment> GetById(int id)
    {
        var creditPaymentEntity = await _dbContext.CreditPayments
            .AsNoTracking()
            .FirstOrDefaultAsync(cp => cp.Id == id);

        return _mapper.Map<CreditPayment>(creditPaymentEntity);
    }

    public async Task<List<CreditPayment>> GetAllByUserCredit(int userId, int creditAccountId)
    {
        var creditPaymentEntitiesList = await _dbContext.CreditPayments
            .AsNoTracking()
            .Where(cp => cp.UserId == userId && cp.CreditAccountId == creditAccountId)
            .ToListAsync();

        return _mapper.Map<List<CreditPayment>>(creditPaymentEntitiesList);
    }

    public async Task<bool> UpdateStatus(int id, string status)
    {
        var number = await _dbContext.CreditPayments
            .Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(cp => cp.Status, status));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditPayments
            .Where(cp => cp.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
