using MyBank.API.DataTransferObjects.CardDto;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CardsController : ControllerBase
{
    private readonly ICardsService _cardsService;
    private readonly IMapper _mapper;
    public CardsController(ICardsService cardsService, IMapper mapper)
    {
        _cardsService = cardsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] CardDto dto)
    {
        var serviceResponse = await _cardsService.Add(_mapper.Map<Card>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int cardId)
    {
        var serviceResponse = await _cardsService.GetById(cardId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId)
    {
        var serviceResponse = await _cardsService.GetAllByUser(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdatePincode([FromBody] UpdatePincodeDto updatePincodeDto)
    {
        var serviceResponse = await _cardsService.UpdatePincode(updatePincodeDto.Id, updatePincodeDto.Pincode);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateName(int cardId, string name)
    {
        var serviceResponse = await _cardsService.UpdateName(cardId, name);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateStatus(int cardId, bool status)
    {
        var serviceResponse = await _cardsService.UpdateStatus(cardId, status);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> Delete(int cardId)
    {
        var serviceResponse = await _cardsService.Delete(cardId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CardsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
