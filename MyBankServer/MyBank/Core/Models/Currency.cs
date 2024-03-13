
namespace MyBank.Core.Models;

public class Currency
{
    public Currency(int id, string code, string name, int scale, DateTime lastDateRateUpdate, decimal officialRate)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        LastDateRateUpdate = lastDateRateUpdate;
        OfficialRate = officialRate;
    }

    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Scale { get; set; }
    public DateTime LastDateRateUpdate { get; set; }
    public decimal OfficialRate { get; set; }
    public List<PersonalAccount> PersonalAccounts { get; set; } = [];
    public List<CreditAccount> CreditAccounts { get; set; } = [];
    public List<DepositAccount> DepositAccounts { get; set; } = [];
}