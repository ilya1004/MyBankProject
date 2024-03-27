using MyBank.API.DataTransferObjects.CurrencyDtos;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
    private readonly IMapper _mapper;
    public CurrencyController(ICurrencyService currencyService, IMapper mapper)
    {
        _currencyService = currencyService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Add([FromBody] CurrencyDto dto)
    {
        var serviceResponse = await _currencyService.Add(_mapper.Map<Currency>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }
        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoById(int currencyId)
    {
        var serviceResponse = await _currencyService.GetById(currencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoByCode(string currencyCode)
    {
        var serviceResponse = await _currencyService.GetByCode(currencyCode);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetAllInfo()
    {
        var serviceResponse = await _currencyService.GetAll();

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int currencyId)
    {
        var serviceResponse = await _currencyService.Delete(currencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}