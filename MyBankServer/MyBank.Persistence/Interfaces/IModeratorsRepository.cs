namespace MyBank.Persistence.Interfaces;

public interface IModeratorsRepository
{
    Task<int> Add(Moderator moderator);
    Task<bool> Delete(int id);
    Task<List<Moderator>> GetAll();
    Task<Moderator> GetById(int id);
    Task<Moderator> GetByLogin(string login);
    Task<bool> IsExistByLogin(string login);
    Task<bool> UpdateInfo(int id, bool isActive);
    Task<bool> UpdateInfo(int id, string nickname);
}