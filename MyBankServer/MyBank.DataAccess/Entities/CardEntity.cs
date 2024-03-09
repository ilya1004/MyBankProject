namespace MyBank.DataAccess.Entities;

public class CardEntity
{
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
    public string Pincode {  get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int CardPackageId { get; set; }
    public CardPackageEntity? CardPackage { get; set; } = null;
    public int UserId { get; set; }
    public UserEntity? User { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccountEntity? PersonalAccount { get; set; } = null;
    public int? CreditAccountId { get; set; } = null;
    public CreditAccountEntity? CreditAccount { get; set; } = null;
}

/*
Unable to create a 'DbContext' of type ''. The exception 'No suitable constructor was found for entity type 'CardEntity'.
The following constructors had parameters that could not be bound to properties of the entity type:
Cannot bind 'cardPackage', 'user', 'personalAccount', 'creditAccount' in 'CardEntity(int id, string name, string number, 
DateTime creationDate, DateTime expirationDate, string accountType, string cvvCode, string pincode, bool isActive, int cardPackageId,
CardPackageEntity cardPackage, int userId, UserEntity user, int? personalAccountId, PersonalAccountEntity personalAccount, int? creditAccountId, CreditAccountEntity creditAccount)'
Note that only mapped properties can be bound to constructor parameters. Navigations to related entities, including references to owned
types, cannot be bound.' was thrown while attempting to create an instance. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
 */