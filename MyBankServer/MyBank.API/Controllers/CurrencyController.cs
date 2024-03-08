using Microsoft.AspNetCore.Mvc;
using MyBank.Core.Models;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CurrencyController : ControllerBase
{
    public List<Currency> listCurrencies = [];

    
    [HttpGet]
    public List<Currency> GetAllCurencies()
    {
        //var (currency, info) = Currency.CreateInstance(1, "qwe", "qwe1", 12, (decimal)1.23);

        return listCurrencies;
    }

    [HttpPost]
    public string? AddCurrency([FromBody] Currency currency)
    {
        listCurrencies.Add(currency);
        return currency?.Name;
    }

}

