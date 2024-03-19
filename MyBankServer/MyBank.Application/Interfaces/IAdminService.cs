using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.Application.Interfaces;

public interface IAdminService
{
    Task<ServiceResponse<Admin>> GetById(int id);
    Task<ServiceResponse<string>> Login(string login, string password);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname);
}