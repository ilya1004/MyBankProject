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
    private readonly ICookieValidator _cookieValidator;

    public DepositAccountsController(IDepositAccountsService depositAccountsService, IMapper mapper, ICookieValidator cookieValidator)
    {
        _depositAccountsService = depositAccountsService;
        _mapper = mapper;
        _cookieValidator = cookieValidator;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] DepositAccountDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _depositAccountsService.Add(id!.Value, dto.Name, dto.DepositStartBalance, dto.InterestRate, 
            dto.DepositTermInDays, dto.IsRevocable, dto.HasCapitalisation, dto.HasInterestWithdrawalOption, dto.CurrencyId);

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
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetInfoByCurrentUser(bool includeData, bool onlyActive)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _depositAccountsService.GetAllByUser(id!.Value, includeData, onlyActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<DepositAccount>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int depositAccountId, bool includeData)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _depositAccountsService.GetById(depositAccountId, includeData);

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

        return Results.Json(new { item = MyJsonConverter<DepositAccount>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId, bool includeData, bool onlyActive)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _depositAccountsService.GetAllByUser(userId, includeData, onlyActive);

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

        return Results.Json(new { list = MyJsonConverter<List<DepositAccount>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateName(int depositAccountId, string name)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

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
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _depositAccountsService.RevokeDeposit(depositAccountId, personalAccountId);

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
