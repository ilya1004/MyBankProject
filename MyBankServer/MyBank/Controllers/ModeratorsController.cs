using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;
using MyBank.Domain.DataTransferObjects.ModeratorDtos;
using Microsoft.AspNetCore.Authorization;
using MyBank.Domain.DataTransferObjects;

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

        return Results.Json(new { moderatorId = serviceResponse.Data }, statusCode: 200);
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

    [HttpGet]
    [Authorize(Policy = "ModeratorAndAdminPolicy")]
    public async Task<IResult> GetInfoById(int moderatorId)
    {
        var serviceResponse = await _moderatorsService.GetById(moderatorId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "ModeratorsController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }
    
    [HttpPut]
    [Authorize(Policy = "ModeratorPolicy")]
    public async Task<IResult> UpdateInfo(int moderatorId, string nickname)
    {
        var serviceResponse = await _moderatorsService.UpdateInfo(moderatorId, nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "ModeratorsController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }
}
