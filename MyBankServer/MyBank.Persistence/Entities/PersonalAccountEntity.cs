namespace MyBank.Persistence.Entities;

public class PersonalAccountEntity
{
    public PersonalAccountEntity() { }
    public PersonalAccountEntity(int id, string name, string number, decimal currentBalance, DateTime creationDate, DateTime closingDate, bool isActive, bool isForTransfersByNickname)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        CreationDate = creationDate;
        ClosingDate = closingDate;
        IsActive = isActive;
        IsForTransfersByNickname = isForTransfersByNickname;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsForTransfersByNickname { get; set; }
    public int? UserId { get; set; } = null;
    public UserEntity? UserOwner { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public CurrencyEntity? Currency { get; set; } = null;
    public List<CardEntity> Cards { get; set; } = [];
}