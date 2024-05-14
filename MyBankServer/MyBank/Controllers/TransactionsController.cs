using MyBank.API.DataTransferObjects.TransactionDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionsService transactionsService, IMapper mapper)
    {
        _transactionsService = transactionsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] TransactionDto dto)
    {
        var serviceResponse = await _transactionsService.Add(dto.PaymentAmount, dto.CurrencyId, dto.Information, dto.TransactionType, 
            dto.AccountSenderNumber, dto.UserSenderNickname, dto.CardRecipientNumber, dto.AccountRecipientNumber, dto.UserRecipientNickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "TransactionsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByPersonalAccountId(string personalAccountNumber)
    {
        var serviceResponse = await _transactionsService.GetAllByPersonalAccountNumber(
            personalAccountNumber
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "TransactionsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByPersonalAccountNumber(string accountNumber, DateTime dateStart, DateTime dateEnd)
    {
        var serviceResponse = await _transactionsService.GetAllByPersonalAccountNumber(accountNumber, dateStart, dateEnd);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "TransactionsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Transaction>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetAll()
    {
        var serviceResponse = await _transactionsService.GetAll();

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "TransactionsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Transaction>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }
}
