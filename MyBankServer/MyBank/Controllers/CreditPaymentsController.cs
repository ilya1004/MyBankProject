using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.CreditPaymentDtos;
using MyBank.Application.Utils;
using MyBank.Domain.Models;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class CreditPaymentsController : ControllerBase
{
    private readonly ICreditPaymentsService _creditPaymentsService;
    private readonly ICookieValidator _cookieValidator;
    private readonly IMapper _mapper;

    public CreditPaymentsController(ICreditPaymentsService creditPaymentsService, IMapper mapper, ICookieValidator cookieValidator)
    {
        _creditPaymentsService = creditPaymentsService;
        _cookieValidator = cookieValidator;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] CreditPaymentDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "PersonalAccountsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _creditPaymentsService.Add(dto.PaymentAmount, dto.PaymentNumber, dto.CreditAccountId, dto.CreditAccountNumber, dto.PersonalAccountId, dto.PersonalAccountNumber, dto.UserNickname, id!.Value);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPaymentsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int creditPaymentId)
    {
        var serviceResponse = await _creditPaymentsService.GetById(creditPaymentId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPaymentsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByCreditAccountId(int creditAccountId)
    {
        var serviceResponse = await _creditPaymentsService.GetAllByCredit(creditAccountId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPaymentsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> UpdateStatus(int id, bool paymentStatus)
    {
        var serviceResponse = await _creditPaymentsService.UpdateStatus(id, paymentStatus);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditPaymentsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
