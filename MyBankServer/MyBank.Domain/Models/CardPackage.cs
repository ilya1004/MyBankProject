namespace MyBank.Domain.Models;

public class CardPackage
{
    public CardPackage() { }

    public CardPackage(
        int id,
        string name,
        decimal price,
        int operationsNumber,
        decimal operationsSum,
        decimal averageAccountBalance,
        decimal monthPayroll
    )
    {
        Id = id;
        Name = name;
        Price = price;
        OperationsNumber = operationsNumber;
        OperationsSum = operationsSum;
        AverageAccountBalance = averageAccountBalance;
        MonthPayroll = monthPayroll;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int OperationsNumber { get; set; }
    public decimal OperationsSum { get; set; }
    public decimal AverageAccountBalance { get; set; }
    public decimal MonthPayroll { get; set; }
    public List<Card> Cards { get; set; } = [];
}
