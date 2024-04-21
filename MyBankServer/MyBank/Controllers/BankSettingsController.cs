using MyBank.API.DataTransferObjects.BankSettingsDto;
using MyBank.API.DataTransferObjects.CardDto;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class BankSettingsController : ControllerBase
{
    private readonly ICookieValidator _cookieValidator;
    private readonly IBankSettingsService _bankSettingsService;
    public BankSettingsController(ICookieValidator cookieValidator, IBankSettingsService bankSettingsService)
    {
        _cookieValidator = cookieValidator;
        _bankSettingsService = bankSettingsService;
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetInfoById(int bankSettingsId)
    {
        var serviceResponse = await _bankSettingsService.GetById(bankSettingsId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "BankSettingsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Update([FromBody] BankSettingsDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "BankSettingsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _bankSettingsService.Update(id!.Value, dto.CreditInterestRate, dto.DepositInterestRate);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "BankSettingsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
