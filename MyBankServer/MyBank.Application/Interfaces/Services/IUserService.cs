namespace MyBank.Application.Interfaces.Services;

public interface IUserService
{
    Task<string> Login(string email, string password);
    Task Register(string email, string password, string nickname,
        string name, string surname, string passportSeries, string passportNumber);
}