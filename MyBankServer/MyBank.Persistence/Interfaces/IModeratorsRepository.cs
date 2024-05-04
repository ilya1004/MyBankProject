namespace MyBank.Persistence.Interfaces;

public interface IModeratorsRepository
{
    Task<int> Add(Moderator moderator);
    Task<bool> Delete(int id);
    Task<List<Moderator>> GetAll(bool includeData, bool onlyActive);
    Task<Moderator> GetById(int id, bool includeData);
    Task<Moderator> GetByLogin(string login);
    Task<bool> IsExistByLogin(string login);
    Task<bool> UpdateInfo(int id, string login, string nickname);
    Task<bool> UpdateStatus(int id, bool isActive);
}
