namespace MyBank.Persistence.Repositories;

public class CardsRepository : ICardsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;

    public CardsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Card card)
    {
        var cardPackageEntity = await _dbContext.CardPackages
            .FirstOrDefaultAsync(cp => cp.Id == card.CardPackageId);

        var userEntity = await _dbContext.Users.
            FirstOrDefaultAsync(u => u.Id == card.UserId);

        var personalAccountEntity = await _dbContext.PersonalAccounts
            .FirstOrDefaultAsync(pa => pa.Id == card.PersonalAccountId);

        var cardEntity = _mapper.Map<CardEntity>(card);

        cardEntity.CardPackage = cardPackageEntity;
        cardEntity.User = userEntity;
        cardEntity.PersonalAccount = personalAccountEntity;

        var item = await _dbContext.Cards.AddAsync(cardEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Card> GetById(int id, bool includeData)
    {
        var cardEntity = includeData == true ? 
            await _dbContext.Cards
                .AsNoTracking()
                .Include(c => c.CardPackage)
                .Include(c => c.PersonalAccount)
                    .ThenInclude(pa => pa!.Currency)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id) :
            await _dbContext.Cards
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Card>(cardEntity);
    }

    public async Task<Card> GetByNumber(string number, bool withPersonalAccount, bool withUser)
    {
        CardEntity? cardEntity = null;
        if (withPersonalAccount && withUser)
        {
            cardEntity = await _dbContext.Cards
                .AsNoTracking()
                .Include(c => c.PersonalAccount)
                    .ThenInclude(pa => pa!.Currency)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Number == number);
        }
        else if (withUser)
        {
            cardEntity = await _dbContext.Cards
                .AsNoTracking()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Number == number);
        }
        else if (withPersonalAccount)
        {
            cardEntity = await _dbContext.Cards
                .AsNoTracking()
                .Include(c => c.PersonalAccount)
                    .ThenInclude(pa => pa!.Currency)
                .FirstOrDefaultAsync(c => c.Number == number);
        }
        else
        {
            cardEntity = await _dbContext.Cards
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Number == number);
        }

        return _mapper.Map<Card>(cardEntity);
    }

    public async Task<List<Card>> GetAllByUser(int userId, bool includeData)
    {
        var cardEntitiesList = includeData == true ?
            await _dbContext.Cards
                .AsNoTracking()
                .Where(c => c.IsActive == true && c.UserId == userId)
                .Include(c => c.CardPackage)
                .Include(c => c.PersonalAccount)
                    .ThenInclude(pa => pa!.Currency)
                .Include(c => c.User)
                .ToListAsync() :
            await _dbContext.Cards
                .AsNoTracking()
                .Where(c => c.IsActive == true && c.UserId == userId)
                .ToListAsync(); 

        return _mapper.Map<List<Card>>(cardEntitiesList);
    }

    public async Task<bool> UpdatePincode(int id, string pincode)
    {
        var number = await _dbContext.Cards
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Pincode, pincode));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateName(int id, string name)
    {
        var number = await _dbContext.Cards
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.Name, name));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateOpersStatus(int id, bool isOperationsAllowed)
    {
        var number = await _dbContext.Cards
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => 
                s.SetProperty(c => c.IsOperationsAllowed, isOperationsAllowed));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.Cards
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => 
                s.SetProperty(c => c.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Cards
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
