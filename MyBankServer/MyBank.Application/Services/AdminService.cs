using MyBank.Application.Interfaces;
using MyBank.Application.Utils;
using MyBank.Domain.Models;
using MyBank.Persistence.Interfaces;
using MyBank.Persistence.Repositories;

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

    public async Task<ServiceResponse<Admin>> GetById(int id)
    {
        var admin = await _adminRepository.GetById(id);

        if (admin == null)
        {
            return new ServiceResponse<Admin> { Status = false, Message = $"Admin with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<Admin> { Status = true, Message = "Success", Data = admin };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname)
    {
        var status = await _adminRepository.UpdateInfo(id, nickname);

        if (status == false)
        {
            return new ServiceResponse<bool> { Status = false, Message = $"Unknown error. Maybe admin with given id ({id}) not found", Data = default };
        }

        return new ServiceResponse<bool> { Status = true, Message = "Success", Data = status };
    }
}
