using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.DepositAccountDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class DepositAccountsController : ControllerBase
{
    private readonly IDepositAccountsService _depositAccountsService;
    private readonly IMapper _mapper;

    public DepositAccountsController(IDepositAccountsService depositAccountsService, IMapper mapper)
    {
        _depositAccountsService = depositAccountsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] DepositAccountDto dto)
    {
        var serviceResponse = await _depositAccountsService.Add(_mapper.Map<DepositAccount>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }
        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int depositAccountId)
    {
        var serviceResponse = await _depositAccountsService.GetById(depositAccountId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId)
    {
        var serviceResponse = await _depositAccountsService.GetAllByUser(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateName(int depositAccountId, string name)
    {
        var serviceResponse = await _depositAccountsService.UpdateName(depositAccountId, name);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Revoke(int depositAccountId, int personalAccountId)
    {
        var serviceResponse = await _depositAccountsService.RevokeDeposit(
            depositAccountId,
            personalAccountId
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> WithdrawInterests(int depositAccountId, int personalAccountId)
    {
        var serviceResponse = await _depositAccountsService.WithdrawInterests(
            depositAccountId,
            personalAccountId
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "DepositAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
