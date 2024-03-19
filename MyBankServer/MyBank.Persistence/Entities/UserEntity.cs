namespace MyBank.Persistence.Entities;

public class UserEntity
{
    public UserEntity(int id, string email, string hashedPassword, string nickname, bool isActive, string name, string surname, string patronymic, string phoneNumber, string passportSeries, string passportNumber, DateTime registrationDate, string citizenship)
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
        Citizenship = citizenship;
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
    public string Citizenship { get; set; } = string.Empty;
    public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
    public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
    public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
    public List<CardEntity> Cards { get; set; } = [];
    public List<CreditRequestEntity> CreditRequests { get; set; } = [];
    public List<CreditPaymentEntity> CreditPayments { get; set; } = [];
    public List<MessageEntity> Messages { get; set; } = [];
}
