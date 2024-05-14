using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.CreditAccountDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class CreditAccountsController : ControllerBase
{
    private readonly ICreditAccountsService _creditAccountsService;
    private readonly IMapper _mapper;
    private readonly ICookieValidator _cookieValidator;

    public CreditAccountsController(ICreditAccountsService creditAccountsService, IMapper mapper, ICookieValidator cookieValidator)
    {
        _creditAccountsService = creditAccountsService;
        _mapper = mapper;
        _cookieValidator = cookieValidator;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> Add([FromBody] CreditAccountDto dto)
    {
        var serviceResponse = await _creditAccountsService.Add(_mapper.Map<CreditAccount>(dto));

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

        return Results.Json(new { creditAccountId = serviceResponse.Data }, statusCode: 200);
    }


    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetInfoByCurrentUser(int creditAccountId, bool includeData)
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

        var serviceResponse = await _creditAccountsService.GetById(creditAccountId, includeData);

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

        return Results.Json(new { item = MyJsonConverter<CreditAccount>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetAllInfoByCurrentUser(bool includeData, bool onlyActive)
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

        var serviceResponse = await _creditAccountsService.GetAllByUser(id!.Value, includeData, onlyActive);

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

        return Results.Json(new { list = MyJsonConverter<List<CreditAccount>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int creditAccountId, bool includeData)
    {
        var serviceResponse = await _creditAccountsService.GetById(creditAccountId, includeData);

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

        return Results.Json(new { item = MyJsonConverter<CreditAccount>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId, bool includeData, bool onlyActive)
    {
        var serviceResponse = await _creditAccountsService.GetAllByUser(userId, includeData, onlyActive);

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

        return Results.Json(new { list = MyJsonConverter<List<CreditAccount>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfo(bool includeData, bool onlyActive)
    {
        var serviceResponse = await _creditAccountsService.GetAll(includeData, onlyActive);

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

        return Results.Json(new { list = MyJsonConverter<List<CreditAccount>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetNextPayment(int creditAccountId)
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

        var serviceResponse = await _creditAccountsService.GetNextPayment(id!.Value, creditAccountId);

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

        return Results.Json(new { item = MyJsonConverter<CreditPayment>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> MakePrepayment(int creditAccountId, int personalAccountId)
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

        var serviceResponse = await _creditAccountsService.MakePrepayment(creditAccountId, personalAccountId);

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

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateName(int creditAccountId, string name)
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

        var serviceResponse = await _creditAccountsService.UpdateName(creditAccountId, name);

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

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }


    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> UpdateStatus(int creditAccountId, bool isActive)
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

        var serviceResponse = await _creditAccountsService.UpdateStatus(creditAccountId, isActive);

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

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
