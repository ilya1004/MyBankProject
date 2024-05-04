namespace MyBank.Persistence.Repositories;

public class CreditPaymentsRepository : ICreditPaymentsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreditPaymentsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(CreditPayment creditPayment)
    {
        var creditAccountEntity = await _dbContext.CreditAccounts.FirstOrDefaultAsync(ca =>
            ca.Id == creditPayment.CreditAccountId
        );

        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.Id == creditPayment.UserId
        );

        var сreditPaymentEntity = _mapper.Map<CreditPaymentEntity>(creditPayment);

        сreditPaymentEntity.CreditAccount = creditAccountEntity;
        сreditPaymentEntity.User = userEntity;

        var item = await _dbContext.CreditPayments.AddAsync(сreditPaymentEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<CreditPayment> GetById(int id)
    {
        var creditPaymentEntity = await _dbContext
            .CreditPayments.AsNoTracking()
            .FirstOrDefaultAsync(cp => cp.Id == id);

        return _mapper.Map<CreditPayment>(creditPaymentEntity);
    }

    public async Task<List<CreditPayment>> GetAllByCredit(int creditAccountId)
    {
        var creditPaymentEntitiesList = await _dbContext
            .CreditPayments.AsNoTracking()
            .Where(cp => cp.CreditAccountId == creditAccountId)
            .ToListAsync();

        return _mapper.Map<List<CreditPayment>>(creditPaymentEntitiesList);
    }

    public async Task<bool> UpdateStatus(int id, bool status)
    {
        var number = await _dbContext
            .CreditPayments.Where(cp => cp.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(cp => cp.Status, status));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.CreditPayments.Where(cp => cp.Id == id).ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
