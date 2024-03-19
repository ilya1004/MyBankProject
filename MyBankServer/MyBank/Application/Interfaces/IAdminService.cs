using MyBank.Application.Utils;
using MyBank.Core.Models;

namespace MyBank.Application.Interfaces;

public interface IAdminService
{
    Task<ServiceResponse<string>> Login(string login, string password);
    Task<Admin> GetById(int id);
    Task<bool> UpdateInfo(int id, string nickname);
}