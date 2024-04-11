﻿namespace MyBank.Application.Services;

public class ModeratorsService : IModeratorsService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IModeratorsRepository _moderatorsRepository;

    public ModeratorsService(
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IModeratorsRepository moderatorsRepository
    )
    {
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _moderatorsRepository = moderatorsRepository;
    }

    public async Task<ServiceResponse<int>> Add(string login, string password, string nickname)
    {
        var isExist = await _moderatorsRepository.IsExistByLogin(login);

        if (isExist)
            return new ServiceResponse<int>
            {
                Status = false,
                Message = "Moderator with given login already exists",
                Data = 0
            };

        var hashedPassword = _passwordHasher.GenerateHash(password);

        var moderator = new Moderator
        {
            Id = 0,
            Login = login,
            HashedPassword = hashedPassword,
            Nickname = nickname,
            IsActive = true,
            CreationDate = DateTime.UtcNow,
        };

        var moderatorId = await _moderatorsRepository.Add(moderator);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = moderatorId
        };
    }

    public async Task<ServiceResponse<(int, string)>> Login(string login, string password)
    {
        var isExist = await _moderatorsRepository.IsExistByLogin(login);

        if (!isExist)
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Модератора с данным логином не найдено",
                Data = (-1, string.Empty)
            };

        var moderator = await _moderatorsRepository.GetByLogin(login);

        var res = _passwordHasher.VerifyPassword(password, moderator.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Неверный логин или пароль",
                Data = (-1, string.Empty)
            };
        }

        var token = _jwtProvider.GenerateToken(moderator);

        return new ServiceResponse<(int, string)>
        {
            Status = true,
            Message = "Success",
            Data = (moderator.Id, token)
        };
    }

    public async Task<ServiceResponse<Moderator>> GetById(int id)
    {
        var moderator = await _moderatorsRepository.GetById(id);

        if (moderator == null)
        {
            return new ServiceResponse<Moderator>
            {
                Status = false,
                Message = $"Moderator with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Moderator>
        {
            Status = true,
            Message = "Success",
            Data = moderator
        };
    }

    public async Task<ServiceResponse<List<Moderator>>> GetAll()
    {
        var list = await _moderatorsRepository.GetAll();

        return new ServiceResponse<List<Moderator>>
        {
            Status = true,
            Message = "Success",
            Data = list
        };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname)
    {
        var status = await _moderatorsRepository.UpdateInfo(id, nickname);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe moderator with given id ({id}) not found",
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
        var status = await _moderatorsRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe moderator with given id ({id}) not found",
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
