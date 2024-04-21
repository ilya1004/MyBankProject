namespace MyBank.Domain.Models;

public class CardPackage
{
    public CardPackage() { }

    public CardPackage(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance)
    {
        Id = id;
        Name = name;
        Price = price;
        OperationsNumber = operationsNumber;
        OperationsSum = operationsSum;
        AverageAccountBalance = averageAccountBalance;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int OperationsNumber { get; set; }
    public decimal OperationsSum { get; set; }
    public decimal AverageAccountBalance { get; set; }
    public bool IsActive { get; set; } = true;
    public List<Card> Cards { get; set; } = [];
}
