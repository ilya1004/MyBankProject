namespace MyBank.Domain.Models;

public class User
{
    public User() { }

    public User(int id, string email, string hashedPassword, string nickname, bool isActive, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, DateTime registrationDate, DateTime birthdayDate, string citizenship, int creditRequestsRejected, string avatarImagePath)
    {
        Id = id;
        Email = email;
        HashedPassword = hashedPassword;
        Nickname = nickname;
        IsActive = isActive;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        PhoneNumber = phoneNumber;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        RegistrationDate = registrationDate;
        BirthdayDate = birthdayDate;
        Citizenship = citizenship;
        CreditRequestsRejected = creditRequestsRejected;
        AvatarImagePath = avatarImagePath;
    }

    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PassportSeries { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime BirthdayDate { get; set; }
    public string Citizenship { get; set; } = string.Empty;
    public int CreditRequestsRejected { get; set; } = 0;
    public string AvatarImagePath { get; set; } = string.Empty;
    public List<PersonalAccount> PersonalAccounts { get; set; } = [];
    public List<CreditAccount> CreditAccounts { get; set; } = [];
    public List<DepositAccount> DepositAccounts { get; set; } = [];
    public List<Card> Cards { get; set; } = [];
    public List<CreditRequest> CreditRequests { get; set; } = [];
    public List<CreditPayment> CreditPayments { get; set; } = [];
    public List<Message> Messages { get; set; } = [];
}
