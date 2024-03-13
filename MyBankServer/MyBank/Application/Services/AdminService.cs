using MyBank.Application.Interfaces;
using MyBank.Core.Models;
using MyBank.Database.Enterfaces;

namespace MyBank.Application.Services;

public class AdminService
{
    private readonly IAdminRepository _adminRepository;
    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
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
