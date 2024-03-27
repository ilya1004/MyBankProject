using MyBank.API.DataTransferObjects.CreditPaymentDtos;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CreditPaymentsController : ControllerBase
{
    private readonly ICreditPaymentsService _creditPaymentsService;
    private readonly IMapper _mapper;
    public CreditPaymentsController(ICreditPaymentsService creditPaymentsService, IMapper mapper)
    {
        _creditPaymentsService = creditPaymentsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] CreditPaymentDto dto)
    {
        var serviceResponse = await _creditPaymentsService.Add(_mapper.Map<CreditPayment>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditPaymentsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
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
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditPaymentsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
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
            return Results.Json(new ErrorDto
            {
                ControllerName = "CreditPaymentsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

}