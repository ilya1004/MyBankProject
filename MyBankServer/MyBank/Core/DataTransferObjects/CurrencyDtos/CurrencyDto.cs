namespace MyBank.Core.DataTransferObjects.CurrencyDtos;

public class CurrencyDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Scale { get; set; }
    public DateTime LastDateRateUpdate { get; set; }
    public decimal OfficialRate { get; set; }
}
