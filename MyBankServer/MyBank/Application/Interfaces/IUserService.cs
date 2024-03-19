using MyBank.Application.Utils;
using MyBank.Core.DataTransferObjects.UserDtos;
using MyBank.Core.Models;

namespace MyBank.Application.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<int>> Register(RegisterUserDto registerUserDto);

    Task<ServiceResponse<string>> Login(string email, string password);

    Task<ServiceResponse<User>> GetById(int id);

    Task<ServiceResponse<List<User>>> GetAll();

    Task<ServiceResponse<bool>> UpdateAccountInfo(int id, string email, string hashedPassword);

    Task<ServiceResponse<bool>> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);

    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);

    Task<ServiceResponse<bool>> Delete(int id);
}