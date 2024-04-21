public class TransactionsRepository : ITransactionsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public TransactionsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Transaction transaction)
    {
        var transactionEntity = _mapper.Map<TransactionEntity>(transaction);

        var item = await _dbContext.Transactions.AddAsync(transactionEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountNumber(string personalAccountNumber)
    {
        var transationEntitiesList = await _dbContext
            .Transactions.AsNoTracking()
            .Where(t =>
                t.AccountSenderNumber == personalAccountNumber
                || t.AccountRecipientNumber == personalAccountNumber
            )
            .ToListAsync();

        return _mapper.Map<List<Transaction>>(transationEntitiesList);
    }

    public async Task<List<Transaction>> GetAllByPersonalAccountDate(
        string personalAccountNumber,
        DateTime dateTimeStart,
        DateTime dateTimeEnd
    )
    {
        var transationEntitiesList = await _dbContext
            .Transactions.AsNoTracking()
            .Where(t => (t.AccountSenderNumber == personalAccountNumber
                        || t.AccountRecipientNumber == personalAccountNumber)
                        && dateTimeStart <= t.Datetime
                        && t.Datetime <= dateTimeEnd)
            .ToListAsync();

        return _mapper.Map<List<Transaction>>(transationEntitiesList);
    }
}
