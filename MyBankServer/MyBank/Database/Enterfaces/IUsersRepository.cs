using MyBank.Core.Models;

namespace MyBank.Database.Enterfaces;

public interface IUsersRepository
{
    Task<int> Add(User user);
    Task<bool> Delete(int id);
    Task<List<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task<User> GetById(int id);
    Task<bool> IsExistByEmail(string email);
    Task<bool> UpdateAccountInfo(int id, string email, string hashedPassword);
    Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);
    Task<bool> UpdateStatus(int id, bool isActive);
}