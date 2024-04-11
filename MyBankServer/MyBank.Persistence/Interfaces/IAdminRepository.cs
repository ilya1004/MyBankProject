namespace MyBank.Persistence.Interfaces;

public interface IAdminRepository
{
    Task<int> Add(Admin admin);
    Task<Admin> GetById(int id);
    Task<Admin> GetByLogin(string login);
    Task<List<Admin>> GetAll();
    Task<bool> UpdateInfo(int id, string nickname);
    Task<bool> Delete(int id);
    Task<bool> IsExistByLogin(string login);
}
