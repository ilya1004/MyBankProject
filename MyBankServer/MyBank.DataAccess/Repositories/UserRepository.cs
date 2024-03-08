using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBank.Core.Models;
using MyBank.DataAccess.Enterfaces;
using MyBank.DataAccess.Entities;

namespace MyBank.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private MyBankDbContext _dbContext;
    private IMapper _mapper;
    public UserRepository(MyBankDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<User> GetUserByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return _mapper.Map<User>(userEntity);
    }

    public async Task AddUser(User user)
    {
        var userEntity = new UserEntity(user.Id, user.Email, user.HashedPassword, user.Nickname, user.IsActive, user.Name, user.Surname, user.Patronymic, user.PhoneNumber, user.PassportSeries, user.PassportNumber, user.RegistrationDate, user.Citizenship); ;

        
    }
}
