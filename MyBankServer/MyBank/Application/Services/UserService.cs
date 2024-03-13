using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    public UserService(IPasswordHasher passwordHasher, IUsersRepository userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<int> Register(string email, string password, string nickname,
        string name, string surname, string passportSeries, string passportNumber)
    {
        var hashedPassword = _passwordHasher.GenerateHash(password);

        var user = new User(0, email, hashedPassword, nickname, true, name, surname, string.Empty, string.Empty, passportSeries, passportNumber, DateTime.UtcNow, string.Empty);

        return await _userRepository.Add(user);
    }

    public async Task<(bool, string)> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        var res = _passwordHasher.VerifyPassword(password, user.HashedPassword);

        if (res == false)
        {
            return (false, "");
        }

        var token = _jwtProvider.GenerateToken(user);

        return (true, token);
    }

    public async Task<User> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<bool> UpdateAccountInfo(int id, string email, string hashedPassword)
    {
        return await _userRepository.UpdateAccountInfo(id, email, hashedPassword);
    }

    public async Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        return await _userRepository.UpdatePersonalInfo(id, nickname, name, surname, patronymic, phoneNumber, passportSeries, passportNumber, citizenship);
    }

    public async Task<bool> UpdateStatus(int id, bool isActive)
    {
        return await _userRepository.UpdateStatus(id, isActive);
    }

    public async Task<bool> Delete(int id)
    {
        return await _userRepository.Delete(id);
    }
}
