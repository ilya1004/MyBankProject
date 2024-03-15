using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Entities;

namespace MyBank.Database.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly MyBankDbContext _dbContext;
    private readonly IMapper _mapper;
    public UsersRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
        return userEntity.Id;
    }

    public async Task<User> GetById(int id)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return _mapper.Map<User>(userEntity);
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return _mapper.Map<User>(userEntity);
    }

    public async Task<List<User>> GetAll()
    {
        var userEntitiesList = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<User>>(userEntitiesList);
    }

    public async Task<bool> IsExistByEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> UpdateAccountInfo(int id, string email, string hashedPassword)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Email, email)
                .SetProperty(u => u.HashedPassword, hashedPassword));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Nickname, nickname)
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.Surname, surname)
                .SetProperty(u => u.PhoneNumber, phoneNumber)
                .SetProperty(u => u.PassportSeries, passportSeries)
                .SetProperty(u => u.PassportNumber, passportNumber)
                .SetProperty(u => u.Citizenship, citizenship));

        return (number == 0) ? false : true;
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.IsActive, isActive));

        return (number == 0) ? false : true;
    }

    public async Task<bool> Delete(int id)
    {
        var number = await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return (number == 0) ? false : true;
    }
}
