using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.PersonalAccountDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class PersonalAccountsController : ControllerBase
{
    private readonly IPersonalAccountsService _personalAccountsService;
    private readonly IMapper _mapper;

    public PersonalAccountsController(
        IPersonalAccountsService personalAccountsService,
        IMapper mapper
    )
    {
        _personalAccountsService = personalAccountsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] PersonalAccountDto dto)
    {
        var serviceResponse = await _personalAccountsService.Add(_mapper.Map<PersonalAccount>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int personalAccountId)
    {
        var serviceResponse = await _personalAccountsService.GetById(personalAccountId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
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
        var serviceResponse = await _personalAccountsService.GetAllByUser(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateName(int personalAccountId, string name)
    {
        var serviceResponse = await _personalAccountsService.UpdateName(personalAccountId, name);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> CloseAccount(int personalAccountId)
    {
        var serviceResponse = await _personalAccountsService.CloseAccount(personalAccountId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> MakeTransfer([FromBody] TransferDto dto)
    {
        var serviceResponse = await _personalAccountsService.MakeTransfer(
            dto.PersonalAccountId,
            dto.AccountSenderNumber,
            dto.UserSenderNickname,
            dto.AccountRecipientNumber,
            dto.CardRecipientNumber,
            dto.UserRecipientNickname,
            dto.Amount
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "PersonalAccountsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
