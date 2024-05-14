using Microsoft.JSInterop.Infrastructure;
using MyBank.API.DataTransferObjects.CreditPackageDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class CreditPackagesController : ControllerBase
{
    private readonly ICreditPackagesService _creditPackagesService;

    public CreditPackagesController(ICreditPackagesService creditPackagesService)
    {
        _creditPackagesService = creditPackagesService;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Add([FromBody] CreditPackageDto dto)
    {
        var serviceResponse = await _creditPackagesService.Add(
            new CreditPackage(0, dto.Name, dto.CreditStartBalance, dto.CreditGrantedAmount, dto.InterestRate,
            dto.InterestCalculationType, dto.CreditTermInDays, dto.HasPrepaymentOption, true, dto.CurrencyId, null));

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }
        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoById(int creditPackageId, bool includeData)
    {
        var serviceResponse = await _creditPackagesService.GetById(creditPackageId, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = MyJsonConverter<CreditPackage>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetAllInfo(bool includeData, bool onlyActive)
    {
        var serviceResponse = await _creditPackagesService.GetAll(includeData, onlyActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<CreditPackage>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateInfo([FromBody] CreditPackageDto dto)
    {
        var serviceResponse = await _creditPackagesService.UpdateInfo(
            dto.Id,
            dto.Name,
            dto.CreditStartBalance,
            dto.CreditGrantedAmount,
            dto.InterestRate,
            dto.InterestCalculationType,
            dto.CreditTermInDays,
            dto.HasPrepaymentOption, 
            dto.CurrencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateStatus(int creditPackageId, bool isActive)
    {
        var serviceResponse = await _creditPackagesService.UpdateStatus(creditPackageId, isActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int creditPackageId)
    {
        var serviceResponse = await _creditPackagesService.Delete(creditPackageId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
