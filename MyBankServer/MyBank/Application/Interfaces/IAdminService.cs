using MyBank.Core.Models;

namespace MyBank.Application.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> GetById(int id);
        Task<bool> UpdateInfo(int id, string nickname);
    }
}