using MyBank.API.DataTransferObjects.TransactionDtos;

namespace MyBank.API.Controllers;

[ApiController]
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
        var serviceResponse = await _transactionsService.Add(_mapper.Map<Transaction>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "TransactionsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByPersonalAccountId(string personalAccountNumber)
    {
        var serviceResponse = await _transactionsService.GetAllByPersonalAccountNumber(personalAccountNumber);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "TransactionsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByPersonalAccountNumber(string personalAccountNumber, DateOnly dateStart, DateOnly dateEnd)
    {
        var serviceResponse = await _transactionsService.GetAllByPersonalAccountNumber(personalAccountNumber, dateStart, dateEnd);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "TransactionsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }
}
