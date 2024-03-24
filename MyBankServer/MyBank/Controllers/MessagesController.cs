using MyBank.API.DataTransferObjects.MessageDtos;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IMapper _mapper;
    public MessagesController(IMessagesService messagesService, IMapper mapper)
    {
        _messagesService = messagesService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> Add([FromBody] MessageDto dto)
    {
        var serviceResponse = await _messagesService.Add(_mapper.Map<Message>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId)
    {
        var serviceResponse = await _messagesService.GetAllByUser(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByModeratorId(int moderatorId)
    {
        var serviceResponse = await _messagesService.GetAllByModerator(moderatorId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetAllInfoByAdminId(int adminId)
    {
        var serviceResponse = await _messagesService.GetAllByAdmin(adminId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> UpdateName(int messageId, bool isRead)
    {
        var serviceResponse = await _messagesService.UpdateIsRead(messageId, isRead);

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
