namespace MyBank.Domain.Models;

public class Admin
{
    public Admin() { }

    public Admin(int id, string login, string hashedPassword, string nickname)
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
    public List<Message> Messages { get; set; } = [];
}
