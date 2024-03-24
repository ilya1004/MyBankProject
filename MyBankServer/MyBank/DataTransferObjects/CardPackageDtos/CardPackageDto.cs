namespace MyBank.API.DataTransferObjects.CardPackageDtos;

public record CardPackageDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int OperationsNumber { get; set; }
    public decimal OperationsSum { get; set; }
    public decimal AverageAccountBalance { get; set; }
    public decimal MonthPayroll { get; set; }
}
