namespace MyBank.Persistence.Entities;

public class ModeratorEntity
{
    public ModeratorEntity() { }
    public ModeratorEntity(int id, string login, string hashedPassword, string nickname, DateTime creationDate, bool isActive)
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
    public List<MessageEntity> Messages { get; set; } = [];
    public List<CreditRequestEntity> CreditRequestsReplied { get; set; } = [];
    public List<CreditAccountEntity> CreditsApproved { get; set; } = [];
}