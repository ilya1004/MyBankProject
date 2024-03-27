namespace MyBank.Domain.Models;

public class Card
{
    public Card(int id, string name, string number, DateTime creationDate, DateTime expirationDate, string cvvCode, string pincode, bool isActive, int? cardPackageId, CardPackage? cardPackage, int? userId, User? user, int? personalAccountId, PersonalAccount? personalAccount)
    {
        Id = id;
        Name = name;
        Number = number;
        CreationDate = creationDate;
        ExpirationDate = expirationDate;
        CvvCode = cvvCode;
        Pincode = pincode;
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
    public bool IsActive { get; set; } = true;
    public int? CardPackageId { get; set; } = null;
    public CardPackage? CardPackage { get; set; } = null;
    public int? UserId { get; set; } = null;
    public User? User { get; set; } = null;
    public int? PersonalAccountId { get; set; } = null;
    public PersonalAccount? PersonalAccount { get; set; } = null;
}