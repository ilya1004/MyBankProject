namespace MyBank.Persistence.Entities;

public class CardEntity
{
    public CardEntity() { }

    public CardEntity(int id, string name, string number, DateTime creationDate, DateTime expirationDate, string cvvCode, string pincode, bool isOperationsAllowed, bool isActive, int? cardPackageId, CardPackageEntity? cardPackage, int? userId, UserEntity? user, int? personalAccountId, PersonalAccountEntity? personalAccount)
    {
        Id = id;
        Name = name;
        Number = number;
        CreationDate = creationDate;
        ExpirationDate = expirationDate;
        CvvCode = cvvCode;
        Pincode = pincode;
        IsOperationsAllowed = isOperationsAllowed;
        IsActive = isActive;
        CardPackageId = cardPackageId;
        CardPackage = cardPackage;
        UserId = userId;
        User = user;
        PersonalAccountId = personalAccountId;
        PersonalAccount = personalAccount;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CvvCode { get; set; } = string.Empty;
    public string Pincode { get; set; } = string.Empty;
    public bool IsOperationsAllowed { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public int? CardPackageId { get; set; } = null;
    public CardPackageEntity? CardPackage { get; set; } = null;
    public int? UserId { get; set; } = null;
    public UserEntity? User { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccountEntity? PersonalAccount { get; set; } = null;
}
