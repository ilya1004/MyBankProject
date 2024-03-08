using MyBank.Application.Interfaces.Services;
using MyBank.Application.Interfaces.Utils;
using MyBank.Application.Utils;
using MyBank.Core.Models;
using MyBank.DataAccess.Enterfaces;

namespace MyBank.Application.Services;

public class UserService : IUserService
{
    private IPasswordHasher _passwordHasher;
    private IUserRepository _userRepository;
    private IJwtProvider _jwtProvider;
    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, JwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task Register(string email, string password, string nickname,
        string name, string surname, string passportSeries, string passportNumber)
    {
        var hashedPassword = _passwordHasher.GenerateHash(password);

        var user = new User(0, email, hashedPassword, nickname, true, name, surname, string.Empty, string.Empty, passportSeries, passportNumber, DateTime.UtcNow, string.Empty);
        //{
        //    Id = 0,
        //    Email = email,
        //    HashedPassword = hashedPassword,
        //    Nickname = nickname,
        //    IsActive = true,
        //    Name = name,
        //    Surname = surname,
        //    Patronymic = string.Empty,
        //    PhoneNumber = string.Empty,
        //    PassportSeries = passportSeries,
        //    PassportNumber = passportNumber,
        //    RegistrationDate = DateTime.UtcNow,
        //    Citizenship = string.Empty
        //};

        await _userRepository.AddUser(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);

        var res = _passwordHasher.VerifyPassword(password, user.HashedPassword);

        if (res == false)
        {
            // false
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}
