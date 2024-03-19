using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;
using MyBank.Database.Repositories;

namespace MyBank.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AdminService(IAdminRepository adminRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<ServiceResponse<string>> Login(string login, string password)
    {
        var admin = await _adminRepository.GetByLogin(login);

        var res = _passwordHasher.VerifyPassword(password, admin.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<string> { Status = false, Message = "Invalid credentials", Data = "" };
        }

        var token = _jwtProvider.GenerateToken(admin);

        return new ServiceResponse<string> { Status = true, Message = "Success", Data = token };
    }

    public async Task<Admin> GetById(int id)
    {
        return await _adminRepository.GetById(id);
    }

    public async Task<bool> UpdateInfo(int id, string nickname)
    {
        return await _adminRepository.UpdateInfo(id, nickname);
    }
}
