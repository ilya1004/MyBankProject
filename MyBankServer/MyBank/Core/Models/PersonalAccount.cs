
namespace MyBank.Core.Models;

public class PersonalAccount
{
    public PersonalAccount(int id, string name, string number, decimal currentBalance, DateTime creationDate, DateTime closingDate, bool isActive, bool isForTransfersByNickname, int userId, User? userOwner, int currencyId, Currency? currency)
    {
        Id = id;
        Name = name;
        Number = number;
        CurrentBalance = currentBalance;
        CreationDate = creationDate;
        ClosingDate = closingDate;
        IsActive = isActive;
        IsForTransfersByNickname = isForTransfersByNickname;
        UserId = userId;
        UserOwner = userOwner;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; } = 0;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsForTransfersByNickname { get; set; }
    public int UserId { get; set; }
    public User? UserOwner { get; set; }
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public List<Transaction> Transactions { get; set; } = [];
    public List<Card> Cards { get; set; } = [];
}
