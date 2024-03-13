namespace MyBank.Application.Interfaces;

public interface IUserService
{
    Task<(bool, string)> Login(string email, string password);
    Task<int> Register(string email, string password, string nickname,
        string name, string surname, string passportSeries, string passportNumber);
    Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);
}