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

    public async Task<ServiceResponse<int>> Register(string email, string password, string nickname, 
        string name, string surname, string patronymic, string passportSeries, string passportNumber, string citizenship)
    {
        var isExist = await _userRepository.IsExistByEmail(email);

        if (isExist)
        {
            return new ServiceResponse<int> { Status = false, Message = "User with given email already exists", Data = 0 };
        }
        
        var hashedPassword = _passwordHasher.GenerateHash(password);

        var user = new User
        {
            Id = 0,
            Email = email,
            HashedPassword = hashedPassword,
            Nickname = nickname,
            IsActive = true,
            Name = name,
            Surname = surname,
            Patronymic = patronymic,
            PhoneNumber = string.Empty,
            PassportSeries = passportSeries,
            PassportNumber = passportNumber,
            RegistrationDate = DateTime.UtcNow,
            Citizenship = citizenship
        };

        var userId = await _userRepository.Add(user);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = userId };
    }

    // Структуры логинов
    // user@mybank.com
    // #moderator#234fk09k
    // #admin#234f234ffr

    public async Task<ServiceResponse<(int, string)>> Login(string email, string password)
    {
        var isExist = await _userRepository.IsExistByEmail(email);

        if (!isExist)
            return new ServiceResponse<(int, string)> { Status = false, Message = "Пользователя с данной электронной почтой не найдено", Data = (-1, string.Empty) };

        var user = await _userRepository.GetByEmail(email);

        var res = _passwordHasher.VerifyPassword(password, user.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<(int, string)> { Status = false, Message = "Неверная электронная почта или пароль", Data = (-1, string.Empty)};
        }

        var token = _jwtProvider.GenerateToken(user);

        return new ServiceResponse<(int, string)> { Status = true, Message = "Success", Data = (user.Id, token) };
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
