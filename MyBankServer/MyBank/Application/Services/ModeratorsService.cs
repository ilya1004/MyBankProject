using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Core.DataTransferObjects.ModeratorDto;
using MyBank.Core.DataTransferObjects.UserDto;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;

namespace MyBank.Application.Services;


public class ModeratorsService : IModeratorsService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IModeratorsRepository _moderatorRepository;

    public ModeratorsService(IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IModeratorsRepository moderatorRepository)
    {
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _moderatorRepository = moderatorRepository;
    }

    public async Task<ServiceResponse<int>> Add(RegisterModeratorDto registerModeratorDto)
    {
        var isExist = await _moderatorRepository.IsExistByLogin(registerModeratorDto.Login);

        if (isExist)
        {
            return new ServiceResponse<int> { Status = false, Message = "Moderator with given login already exists", Data = 0 };
        }

        var hashedPassword = _passwordHasher.GenerateHash(registerModeratorDto.Password);

        var moderator = new Moderator
        {
            Id = 0,
            Login = registerModeratorDto.Login,
            HashedPassword = hashedPassword,
            Nickname = registerModeratorDto.Nickname,
            IsActive = true,
            CreationDate = DateTime.UtcNow,
        };

        var userId = await _moderatorRepository.Add(moderator);

        return new ServiceResponse<int> { Status = true, Message = "Success", Data = userId };
    }

    public async Task<ServiceResponse<string>> Login(string login, string password)
    {
        var moderator = await _moderatorRepository.GetByLogin(login);

        var res = _passwordHasher.VerifyPassword(password, moderator.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<string> { Status = false, Message = "Invalid credentials", Data = "" };
        }

        var token = _jwtProvider.GenerateToken(moderator);

        return new ServiceResponse<string> { Status = true, Message = "Success", Data = token };
    }

    public async Task<ServiceResponse<Moderator>> GetById(int id)
    {
        var moderator = await _moderatorRepository.GetById(id);

        if (moderator == null)
        {
            return new ServiceResponse<Moderator> { Status = false, Message = $"Moderator with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<Moderator> { Status = true, Message = "Success", Data = moderator };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname)
    {
        var status = await _moderatorRepository.UpdateInfo(id, nickname);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe moderator with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
