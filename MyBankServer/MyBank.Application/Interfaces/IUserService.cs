using Microsoft.AspNetCore.Http;

namespace MyBank.Application.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<int>> Register(string email, string password, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);
    Task<ServiceResponse<(int, string)>> Login(string email, string password);
    Task<ServiceResponse<bool>> UploadAvatarFile(IFormFile file, int id);
    Task<ServiceResponse<bool>> IsExistByEmail(string email);
    Task<ServiceResponse<User>> GetById(int id, bool includeData);
    Task<ServiceResponse<List<User>>> GetAll(bool includeData);
    Task<ServiceResponse<string>> GetAvatarImagePath(int id);
    Task<ServiceResponse<bool>> UpdatePassword(int id, string oldEmail, string oldPassword, string newPassword);
    Task<ServiceResponse<bool>> UpdateEmail(int id, string oldEmail, string oldPassword, string newEmail);
    Task<ServiceResponse<bool>> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);
    Task<ServiceResponse<bool>> UpdateStatus(int id, bool isActive);
    Task<ServiceResponse<bool>> Delete(int id);   
}