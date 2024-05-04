using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CurrencyEntity
{
    public CurrencyEntity() { }

    public CurrencyEntity(int id, string code, string name, int scale, DateTime lastRateUpdate, decimal officialRate, bool isActive)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        LastRateUpdate = lastRateUpdate;
        OfficialRate = officialRate;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Scale { get; set; }
    public DateTime LastRateUpdate { get; set; }
    [Column(TypeName = "money")]
    public decimal OfficialRate { get; set; }
    public bool IsActive { get; set; } = true;
    public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
    public List<CreditPackageEntity> CreditPackages { get; set; } = [];
    public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
    public List<DepositPackageEntity> DepositPackages { get; set; } = [];
    public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
}
