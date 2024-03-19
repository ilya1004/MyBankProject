using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;

namespace MyBank.API.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class CardPackagesController
{
    private readonly ICardPackagesService _cardPackagesService;
    public CardPackagesController(ICardPackagesService cardPackagesService)
    {
        _cardPackagesService = cardPackagesService;
    }
}
