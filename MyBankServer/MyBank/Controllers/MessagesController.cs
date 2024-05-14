using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.MessageDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IMapper _mapper;
    private readonly ICookieValidator _cookieValidator;

    public MessagesController(IMessagesService messagesService, IMapper mapper, ICookieValidator cookieValidator)
    {
        _messagesService = messagesService;
        _mapper = mapper;
        _cookieValidator = cookieValidator;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> Add([FromBody] MessageDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _messagesService.Add(id!.Value, role!, dto.Title, dto.Text, dto.RecepientId, dto.RecepientNickname, dto.RecepientRole);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId, bool? isRead)
    {
        var serviceResponse = await _messagesService.GetAllByUser(userId, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByModeratorId(int moderatorId, bool? isRead)
    {
        var serviceResponse = await _messagesService.GetAllByModerator(moderatorId, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetAllInfoByAdminId(int adminId, bool? isRead)
    {
        var serviceResponse = await _messagesService.GetAllByAdmin(adminId, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetAllInfoByCurrentUser(bool? isRead)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _messagesService.GetAllByUser(id!.Value, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorPolicy)]
    public async Task<IResult> GetAllInfoByCurrentModerator(bool? isRead)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _messagesService.GetAllByModerator(id!.Value, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetAllInfoByCurrentAdmin(bool? isRead)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _messagesService.GetAllByAdmin(id!.Value, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Message>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> UpdateIsRead(int messageId, bool isRead)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "MessagesController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _messagesService.UpdateIsRead(messageId, isRead);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "MessagesController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
