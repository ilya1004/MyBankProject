using Microsoft.AspNetCore.Http;
using MyBank.Domain.Models;

namespace MyBank.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IModeratorsRepository _moderatorRepository;

    public UserService(
        IPasswordHasher passwordHasher,
        IUsersRepository userRepository,
        IJwtProvider jwtProvider,
        IModeratorsRepository moderatorRepository
    )
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _moderatorRepository = moderatorRepository;
    }

    public async Task<ServiceResponse<int>> Register(string email, string password, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        var isExist = await _userRepository.IsExistByEmail(email);

        if (isExist)
        {
            return new ServiceResponse<int>
            {
                Status = false,
                Message = "User with given email already exists",
                Data = 0
            };
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
            PhoneNumber = phoneNumber,
            PassportSeries = passportSeries,
            PassportNumber = passportNumber,
            RegistrationDate = DateTime.UtcNow,
            Citizenship = citizenship,
            AvatarImagePath = "C:\\Users\\user\\source\\repos\\OOP_Project\\MyBankServer\\MyBank.Application\\Files\\Avatars\\avatar-placeholder.png"
        };

        var userId = await _userRepository.Add(user);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = userId
        };
    }

    /*
    Структура логинов:
    user@mybank.com
    #moderator#123qwe
    #admin#123qwe
    */

    public async Task<ServiceResponse<(int, string)>> Login(string email, string password)
    {
        var isExist = await _userRepository.IsExistByEmail(email);

        if (!isExist)
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Пользователя с данной электронной почтой не найдено",
                Data = (-1, string.Empty)
            };

        var user = await _userRepository.GetByEmail(email);

        var res = _passwordHasher.VerifyPassword(password, user.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Неверная электронная почта или пароль",
                Data = (-1, string.Empty)
            };
        }

        var token = _jwtProvider.GenerateToken(user);

        return new ServiceResponse<(int, string)>
        {
            Status = true,
            Message = "Success",
            Data = (user.Id, token)
        };
    }

    public async Task<ServiceResponse<bool>> UploadAvatarFile(IFormFile file, int id)
    {
        if (file == null || file.Length == 0)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Вы выбрали неверный файл",
                Data = false,
            };
        }

        const string filePath = "C:\\Users\\user\\source\\repos\\OOP_Project\\MyBankServer\\MyBank.Application\\Files\\Avatars\\";

        var fileExt = Path.GetExtension(file.FileName);
        var fileFullPath = filePath + $"userAvatar_{id}" + fileExt;

        using (var stream = new FileStream(fileFullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var status = await _userRepository.UpdateAvatarPath(id, fileFullPath);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Произошла ошибка при сохранении аватара профиля",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<User>> GetById(int id, bool includeData)
    {
        var user = await _userRepository.GetById(id, includeData);

        if (user == null)
        {
            return new ServiceResponse<User>
            {
                Status = false,
                Message = $"User with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<User>
        {
            Status = true,
            Message = "Success",
            Data = user
        };
    }

    public async Task<ServiceResponse<List<User>>> GetAll(bool includeData)
    {
        var list = await _userRepository.GetAll(includeData);

        return new ServiceResponse<List<User>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<string>> GetAvatarImagePath(int id)
    {
        var user = await _userRepository.GetById(id, false);

        var imagePath = user.AvatarImagePath;

        return new ServiceResponse<string>
        {
            Status = true,
            Message = "Success",
            Data = imagePath
        };
    }

    public async Task<ServiceResponse<bool>> UpdatePassword(int id, string oldEmail, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetById(id, false);

        if (user.Email != oldEmail) {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Неверная элекронная почта",
                Data = default
            };
        }

        var res = _passwordHasher.VerifyPassword(oldPassword, user.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Неверный пароль",
                Data = default
            };
        }

        var hashedPassword = _passwordHasher.GenerateHash(newPassword);

        var status = await _userRepository.UpdatePassword(id, hashedPassword);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe user with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateEmail(int id, string oldEmail, string oldPassword, string newEmail)
    {
        var user = await _userRepository.GetById(id, false);

        if (user.Email == oldEmail)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Неверная элекронная почта",
                Data = default
            };
        }

        var res = _passwordHasher.VerifyPassword(oldPassword, user.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = "Неверный пароль",
                Data = default
            };
        }

        var status = await _userRepository.UpdateEmail(id, newEmail);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe user with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship)
    {
        var status = await _userRepository.UpdatePersonalInfo(id, nickname, name, surname, patronymic, phoneNumber, passportSeries, passportNumber, citizenship);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe user with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive)
    {
        var status = await _userRepository.UpdateStatus(id, isActive);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe user with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _userRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe user with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
