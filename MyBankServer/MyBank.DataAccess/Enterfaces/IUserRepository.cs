using MyBank.Core.Models;

namespace MyBank.DataAccess.Enterfaces;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task AddUser(User user);
}