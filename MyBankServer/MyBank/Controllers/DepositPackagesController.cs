using MyBank.API.DataTransferObjects.CreditPackageDtos;
using MyBank.API.DataTransferObjects.DepositPackageDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class DepositPackagesController
{
    private readonly IDepositPackagesService _depositPackagesService;

    public DepositPackagesController(IDepositPackagesService depositPackagesService)
    {
        _depositPackagesService = depositPackagesService;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Add([FromBody] DepositPackageDto dto)
    {
        var serviceResponse = await _depositPackagesService.Add(dto.Name, dto.DepositStartBalance, dto.InterestRate, 
            dto.DepositTermInDays, dto.IsRevocable, dto.HasCapitalisation, dto.HasInterestWithdrawalOption, dto.CurrencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }
        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoById(int depositPackageId, bool includeData)
    {
        var serviceResponse = await _depositPackagesService.GetById(depositPackageId, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = MyJsonConverter<DepositPackage>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetAllInfo(bool includeData, bool onlyActive)
    {
        var serviceResponse = await _depositPackagesService.GetAll(includeData, onlyActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<DepositPackage>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateInfo([FromBody] DepositPackageDto dto)
    {
        var serviceResponse = await _depositPackagesService.UpdateInfo(dto.Id, dto.Name, dto.DepositStartBalance, dto.InterestRate,
            dto.DepositTermInDays, dto.IsRevocable, dto.HasCapitalisation, dto.HasInterestWithdrawalOption, dto.CurrencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> UpdateStatus(int depositPackageId, bool isActive)
    {
        var serviceResponse = await _depositPackagesService.UpdateStatus(depositPackageId, isActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int depositPackageId)
    {
        var serviceResponse = await _depositPackagesService.Delete(depositPackageId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositPackagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
