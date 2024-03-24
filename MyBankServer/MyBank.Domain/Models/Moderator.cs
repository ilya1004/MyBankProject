namespace MyBank.Domain.Models;

public class Moderator
{
    public Moderator() { }
    public Moderator(int id, string login, string hashedPassword, string nickname, DateTime creationDate, bool isActive)
    {
        Id = id;
        Login = login;
        HashedPassword = hashedPassword;
        Nickname = nickname;
        CreationDate = creationDate;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public bool IsActive { get; set; }
    public List<Message> Messages { get; set; } = [];
    public List<CreditRequest> CreditRequestsReplied { get; set; } = [];
    public List<CreditAccount> CreditsApproved { get; set; } = [];
}
