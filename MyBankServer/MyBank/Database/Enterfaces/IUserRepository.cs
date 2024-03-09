using MyBank.Core.Models;

namespace MyBank.Database.Enterfaces;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task AddUser(User user);
}