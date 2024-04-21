using System.ComponentModel.DataAnnotations.Schema;

namespace MyBank.Persistence.Entities;

public class CardPackageEntity
{
    public CardPackageEntity() { }

    public CardPackageEntity(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, bool isActive)
    {
        Id = id;
        Name = name;
        Price = price;
        OperationsNumber = operationsNumber;
        OperationsSum = operationsSum;
        AverageAccountBalance = averageAccountBalance;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public int OperationsNumber { get; set; }
    [Column(TypeName = "money")]
    public decimal OperationsSum { get; set; }
    [Column(TypeName = "money")]
    public decimal AverageAccountBalance { get; set; }
    public bool IsActive { get; set; } = true;
    public List<CardEntity> Cards { get; set; } = [];
}
