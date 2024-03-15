using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;
using MyBank.Core.DataTransferObjects;
using MyBank.Core.DataTransferObjects.ModeratorDto;
using Microsoft.AspNetCore.Authorization;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ModeratorsController : ControllerBase
{
    private readonly IModeratorsService _moderatorsService;
    public ModeratorsController(IModeratorsService moderatorsService)
    {
        _moderatorsService = moderatorsService;
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IResult> Add([FromBody] RegisterModeratorDto request)
    {
        var serviceResponse = await _moderatorsService.Add(request);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "ModeratorsController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginModeratorDto request)
    {
        var serviceResponse = await _moderatorsService.Login(request.Login, request.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "ModeratorsController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        Response.Cookies.Append("my-cookie", serviceResponse.Data!, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { jwtToken = serviceResponse.Data! }, statusCode: 200);
    }
}
