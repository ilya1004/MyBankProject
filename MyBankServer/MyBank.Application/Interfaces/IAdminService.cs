namespace MyBank.Application.Interfaces;

public interface IAdminService
{
    Task<ServiceResponse<int>> Register(string login, string password, string nickname);
    Task<ServiceResponse<Admin>> GetById(int id);
    Task<ServiceResponse<(int, string)>> Login(string login, string password);
    Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname);
    Task<ServiceResponse<bool>> Delete(int id);
}
