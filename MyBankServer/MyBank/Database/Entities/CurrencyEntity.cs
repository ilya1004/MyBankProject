namespace MyBank.Database.Entities;

public class CurrencyEntity
{
    public CurrencyEntity() { }
    public CurrencyEntity(int id, string code, string name, int scale, DateTime lastDateRateUpdate, decimal officialRate)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        LastDateRateUpdate = lastDateRateUpdate;
        OfficialRate = officialRate;
    }

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Scale { get; set; }
    public DateTime LastDateRateUpdate { get; set; }
    public decimal OfficialRate { get; set; }
    public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
    public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
    public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
}