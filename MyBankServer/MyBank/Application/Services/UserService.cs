using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Core.DataTransferObjects.UserDto;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IModeratorsRepository _moderatorRepository;

    public UserService(IPasswordHasher passwordHasher, IUsersRepository userRepository,  IJwtProvider jwtProvider, IModeratorsRepository moderatorRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _moderatorRepository = moderatorRepository;
    }

    public async Task<ServiceResponse<int>> Register(RegisterUserDto registerUserDto)
    {
        var isExist = await _userRepository.IsExistByEmail(registerUserDto.Email);

        if (isExist)
        {
            return new ServiceResponse<int> { Status = false, Message = "User with given email already exists", Data = 0 };
        }
        
        var hashedPassword = _passwordHasher.GenerateHash(registerUserDto.Password);

        var user = new User
        {
            Id = 0,
            Email = registerUserDto.Email,
            HashedPassword = hashedPassword,
            Nickname = registerUserDto.Nickname,
            IsActive = true,
            Name = registerUserDto.Name,
            Surname = registerUserDto.Surname,
            Patronymic = string.Empty,
            PhoneNumber = string.Empty,
            PassportSeries = registerUserDto.PassportSeries,
            PassportNumber = registerUserDto.PassportNumber,
            RegistrationDate = DateTime.UtcNow,
            Citizenship = registerUserDto.Citizenship
        };

        var userId = await _userRepository.Add(user);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = userId };
    }

    // Структуры логинов
    // user@mybank.com
    // #moderator#234fk09k
    // #admin#234f234ffr

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        if (email[0] == '#' && !email.Contains('@'))
        {
            if (email.Substring(1, 9) == "moderator")
            {
                var moderator = await _moderatorRepository.GetByLogin(email);
            } 
            else if (email.Substring(1, 5) == "admin") 
            {
                var moderator = await _moderatorRepository.GetByLogin(email);
            }
        };

        var user = await _userRepository.GetByEmail(email);

        var res = _passwordHasher.VerifyPassword(password, user.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<string> { Status = false, Message = "Invalid credentials", Data = ""};
        }

        var token = _jwtProvider.GenerateToken(user);

        return new ServiceResponse<string> { Status = true, Message = "Success", Data = token };
    }

    public async Task<ServiceResponse<User>> GetById(int id)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
        {
            return new ServiceResponse<User> { Status = false, Message = $"User with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<User> { Status = true, Message = "Success", Data = user };
    }

    public async Task<ServiceResponse<List<User>>> GetAll()
    {
        var list = await _userRepository.GetAll();

        return new ServiceResponse<List<User>> { Status = true, Message = "Success", Data = list };
    }

    public async Task<ServiceResponse<bool>> UpdateAccountInfo(int id, string email, string password)
    {
        var hashedPassword = _passwordHasher.GenerateHash(password);

        var status = await _userRepository.UpdateAccountInfo(id, email, hashedPassword);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe user with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        var status = await _userRepository.UpdatePersonalInfo(id, nickname, name, surname, patronymic, phoneNumber, passportSeries, passportNumber, citizenship);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe user with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive)
    {
        var status = await _userRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe user with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _userRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe user with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
