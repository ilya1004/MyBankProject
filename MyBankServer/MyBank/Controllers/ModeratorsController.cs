using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.ModeratorDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class ModeratorsController : ControllerBase
{
    private readonly IModeratorsService _moderatorsService;
    private readonly ICookieValidator _cookieValidator;

    public ModeratorsController(IModeratorsService moderatorsService, ICookieValidator cookieValidator)
    {
        _moderatorsService = moderatorsService;
        _cookieValidator = cookieValidator;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Add([FromBody] RegisterModeratorDto dto)
    {
        var serviceResponse = await _moderatorsService.Add(dto.Login, dto.Password, dto.Nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginModeratorDto dto)
    {
        var serviceResponse = await _moderatorsService.Login(dto.Login, dto.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        Response.Cookies.Append(
            "my-cookie",
            serviceResponse.Data.Item2,
            new CookieOptions { SameSite = SameSiteMode.Lax }
        );

        return Results.Json(new { id = serviceResponse.Data.Item1 }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorPolicy)]
    public async Task<IResult> GetInfoCurrent(bool includeData)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "ModeratorsController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _moderatorsService.GetById(id!.Value, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = MyJsonConverter<Moderator>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetInfoById(int moderatorId, bool includeData)
    {
        var serviceResponse = await _moderatorsService.GetById(moderatorId, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = MyJsonConverter<Moderator>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetAllInfo(bool includeData, bool onlyActive)
    {
        var serviceResponse = await _moderatorsService.GetAll(includeData, onlyActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = MyJsonConverter<List<Moderator>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.ModeratorPolicy)]
    public async Task<IResult> UpdateInfo(int moderatorId, string nickname)
    {
        var serviceResponse = await _moderatorsService.UpdateInfo(moderatorId, nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int moderatorId)
    {
        var serviceResponse = await _moderatorsService.Delete(moderatorId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "ModeratorsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
