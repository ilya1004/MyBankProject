namespace MyBank.Persistence.Entities;

public class AdminEntity
{
    public AdminEntity() { }

    public AdminEntity(int id, string login, string hashedPassword, string nickname)
    {
        Id = id;
        Login = login;
        HashedPassword = hashedPassword;
        Nickname = nickname;
    }

    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public List<MessageEntity> Messages { get; set; } = [];
}
