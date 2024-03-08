using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.DataAccess.Entities;

public class CurrencyEntity
{
    public CurrencyEntity(int id, string code, string name, int scale, decimal officialRate)
    {
        Id = id;
        Code = code;
        Name = name;
        Scale = scale;
        OfficialRate = officialRate;
    }
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Scale { get; set; }
    public decimal OfficialRate { get; set; }
    public List<PersonalAccountEntity> PersonalAccounts { get; set; } = [];
    public List<CreditAccountEntity> CreditAccounts { get; set; } = [];
    public List<DepositAccountEntity> DepositAccounts { get; set; } = [];
}