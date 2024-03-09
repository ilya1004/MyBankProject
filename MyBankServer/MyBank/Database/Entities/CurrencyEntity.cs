namespace MyBank.Database.Entities;

public class CurrencyEntity
{
    public CurrencyEntity(int id, string code, string name, int scale, DateTime dateRate, decimal officialRate)
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
    public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
    public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
    public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
}