
namespace MyBank.Core.Models;

public class Currency
{
    public Currency(int id, string code, string name, int scale, DateTime dateRate, decimal officialRate)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        DateRate = dateRate;
        OfficialRate = officialRate;
    }

    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Scale { get; set; }
    public DateTime DateRate { get; set; }
    public decimal OfficialRate { get; set; }
    public List<PersonalAccount> PersonalAccounts { get; set; } = [];
    public List<CreditAccount> CreditAccounts { get; set; } = [];
    public List<DepositAccount> DepositAccounts { get; set; } = [];
}