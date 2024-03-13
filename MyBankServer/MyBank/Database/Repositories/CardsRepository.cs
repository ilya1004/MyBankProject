using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class CardsRepository : ICardsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public CardsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> Add(Card card, int cardPackageId, int userId, int personalAccountId)
    {

        var cardPackageEntity = await _dbContext.CardPackages
            .FirstOrDefaultAsync(cp => cp.Id == cardPackageId);

        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var personalAccountEntity = await _dbContext.PersonalAccounts
                .FirstOrDefaultAsync(pa => pa.Id == personalAccountId);

        var cardEntity = new CardEntity
        {
            Id = 0,
            Name = card.Name,
            Number = card.Number,
            CreationDate = card.CreationDate,
            ExpirationDate = card.ExpirationDate,
            AccountType = card.AccountType,
            CvvCode = card.CvvCode,
            Pincode = card.Pincode,
            IsActive = card.IsActive,
            CardPackageId = cardPackageId,
            CardPackage = cardPackageEntity,
            UserId = userId,
            User = userEntity,
            PersonalAccountId = personalAccountId,
            PersonalAccount = personalAccountEntity
        };

        await _dbContext.Cards.AddAsync(cardEntity);
        var number = await _dbContext.SaveChangesAsync();
        return number == 0 ? false : true;
    }

    public async Task<Card> GetById(int id)
    {
        var cardEntity = await _dbContext.Cards
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Card>(cardEntity);
    }

    public async Task<Card> GetByNumber(string number)
    {
        var cardEntity = await _dbContext.Cards
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Number == number);

        return _mapper.Map<Card>(cardEntity);
    }

    public async Task<List<Card>> GetAllByUser(int userId)
    {
        var cardsList = await _dbContext.Cards
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<Card>>(cardsList);
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
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Name, name));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.Cards
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.IsActive, isActive));

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
