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
                0,
                cardPackageDto.Name,
                cardPackageDto.Price,
                cardPackageDto.OperationsNumber,
                cardPackageDto.OperationsSum,
                cardPackageDto.AverageAccountBalance
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
    public async Task<IResult> GetAllInfo(bool onlyActive)
    {
        var serviceResponse = await _cardPackagesService.GetAll(onlyActive);

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
    public async Task<IResult> UpdateInfo([FromBody] CardPackageDto cardPackageDto)
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

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateStatus(int cardPackageId, bool isActive)
    {
        var serviceResponse = await _cardPackagesService.UpdateStatus(cardPackageId, isActive);

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
