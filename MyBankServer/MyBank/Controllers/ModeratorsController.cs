using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.ModeratorDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class ModeratorsController : ControllerBase
{
    private readonly IModeratorsService _moderatorsService;

    public ModeratorsController(IModeratorsService moderatorsService)
    {
        _moderatorsService = moderatorsService;
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
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int moderatorId)
    {
        var serviceResponse = await _moderatorsService.GetById(moderatorId);

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

        return Results.Json(new { item = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
    public async Task<IResult> GetAllInfo()
    {
        var serviceResponse = await _moderatorsService.GetAll();

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

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
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
