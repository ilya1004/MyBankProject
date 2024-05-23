namespace MyBank.Persistence.Interfaces;

public interface IUsersRepository
{
    Task<int> Add(User user);
    Task<bool> Delete(int id);
    Task<List<User>> GetAll(bool includeData);
    Task<User> GetByEmail(string email);
    Task<User> GetById(int id, bool includeData);
    Task<bool> IsExistByEmail(string email);
    Task<bool> IsExistByNickname(string nickname);
    Task<bool> UpdatePassword(int id, string hashedPassword);
    Task<bool> UpdateEmail(int id, string email);
    Task<bool> UpdateCreditRequestRejected(int id, int creditRequestRejectedNumber);
    Task<bool> UpdatePersonalInfo(int id, string nickname, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, string citizenship);
    Task<bool> UpdateStatus(int id, bool isActive);
    Task<bool> UpdateAvatarPath(int id, string fileFullPath);
}
