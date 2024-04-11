using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.CardPackageDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class CardPackagesController : ControllerBase
{
    private readonly ICardPackagesService _cardPackagesService;

    public CardPackagesController(ICardPackagesService cardPackagesService)
    {
        _cardPackagesService = cardPackagesService;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Add([FromBody] CardPackageDto cardPackageDto)
    {
        var serviceResponse = await _cardPackagesService.Add(
            new CardPackage(
                cardPackageDto.Id,
                cardPackageDto.Name,
                cardPackageDto.Price,
                cardPackageDto.OperationsNumber,
                cardPackageDto.OperationsSum,
                cardPackageDto.AverageAccountBalance,
                cardPackageDto.MonthPayroll
            )
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CardPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }
        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoById(int cardPackageId)
    {
        var serviceResponse = await _cardPackagesService.GetById(cardPackageId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CardPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetAllInfo()
    {
        var serviceResponse = await _cardPackagesService.GetAll();

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CardPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateInfo(CardPackageDto cardPackageDto)
    {
        var serviceResponse = await _cardPackagesService.UpdateInfo(
            cardPackageDto.Id,
            cardPackageDto.Name,
            cardPackageDto.Price,
            cardPackageDto.OperationsNumber,
            cardPackageDto.OperationsSum,
            cardPackageDto.AverageAccountBalance,
            cardPackageDto.MonthPayroll
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CardPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int cardPackageId)
    {
        var serviceResponse = await _cardPackagesService.Delete(cardPackageId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CardPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
