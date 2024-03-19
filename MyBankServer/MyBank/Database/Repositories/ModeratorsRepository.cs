using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class ModeratorsRepository : IModeratorsRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public ModeratorsRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(Moderator moderator)
    {
        var moderatorEntity = new ModeratorEntity
        {
            Id = 0,
            Login = moderator.Login,
            HashedPassword = moderator.HashedPassword,
            Nickname = moderator.Nickname,
            CreationDate = moderator.CreationDate,
            IsActive = moderator.IsActive,
            Messages = []
        };

        var item = await _dbContext.Moderators.AddAsync(moderatorEntity);
        await _dbContext.SaveChangesAsync();
        return item.Entity.Id;
    }

    public async Task<Moderator> GetByLogin(string login)
    {
        var moderatorEntity = await _dbContext.Moderators
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Login == login);

        return _mapper.Map<Moderator>(moderatorEntity);
    }

    public async Task<Moderator> GetById(int id)
    {
        var moderatorEntity = await _dbContext.Moderators
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        return _mapper.Map<Moderator>(moderatorEntity);
    }

    public async Task<List<Moderator>> GetAll()
    {
        var moderatorEntitiesList = await _dbContext.Moderators
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Moderator>>(moderatorEntitiesList);
    }

    public async Task<bool> IsExistByLogin(string login)
    {
        return await _dbContext.Moderators.AnyAsync(m => m.Login == login);
    }

    public async Task<bool> UpdateInfo(int id, string nickname)
    {
        var number = await _dbContext.Moderators
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.Nickname, nickname));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateInfo(int id, bool isActive)
    {
        var number = await _dbContext.Moderators
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Moderators
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
