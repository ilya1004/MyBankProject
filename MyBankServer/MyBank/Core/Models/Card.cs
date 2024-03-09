namespace MyBank.Core.Models;

public class Card
{
    public Card(int id, string name, string number, DateTime creationDate, DateTime expirationDate, string accountType, string cvvCode, string pincode, bool isActive, int cardPackageId, CardPackage? cardPackage, int userId, User? user, int? personalAccountId, PersonalAccount? personalAccount, int? creditAccountId, CreditAccount? creditAccount)
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
        CardPackageId = cardPackageId;
        CardPackage = cardPackage;
        UserId = userId;
        User = user;
        PersonalAccountId = personalAccountId;
        PersonalAccount = personalAccount;
        CreditAccountId = creditAccountId;
        CreditAccount = creditAccount;
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
    public int CardPackageId { get; set; }
    public CardPackage? CardPackage { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int? PersonalAccountId { get; set; }
    public PersonalAccount? PersonalAccount { get; set; } = null;
    public int? CreditAccountId { get; set; }
    public CreditAccount? CreditAccount { get; set; } = null;
}