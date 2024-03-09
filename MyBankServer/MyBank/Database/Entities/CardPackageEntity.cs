using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Database.Entities;

public class CardPackageEntity
{
    public CardPackageEntity(int id, string name, decimal price, int operationsNumber, decimal operationsSum, decimal averageAccountBalance, decimal monthPayroll)
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
    public List<CardEntity> Cards { get; set; } = [];
}