namespace MyBank.Persistence.Entities;

public class CardEntity
{
    public CardEntity() { }
    public CardEntity(int id, string name, string number, DateTime creationDate, DateTime expirationDate, string accountType, string cvvCode, string pincode, bool isActive)
    {
        Id = id;
        Name = name;
        Number = number;
        CreationDate = creationDate;
        ExpirationDate = expirationDate;
        AccountType = accountType;
        CvvCode = cvvCode;
        Pincode = pincode;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string AccountType { get; set; } = string.Empty;
    public string CvvCode { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int? CardPackageId { get; set; } = null;
    public CardPackageEntity? CardPackage { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccountEntity? PersonalAccount { get; set; } = null;
}
