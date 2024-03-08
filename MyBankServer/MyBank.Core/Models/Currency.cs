
namespace MyBank.Core.Models;

public class Currency
{
    public Currency(int id, string code, string name, int scale, decimal officialRate)
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
}