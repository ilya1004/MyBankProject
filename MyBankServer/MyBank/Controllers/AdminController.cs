using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Domain.DataTransferObjects;
using MyBank.Domain.DataTransferObjects.AdminDtos;
using MyBank.Domain.Models;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    /*
    [HttpPost]
    public async Task<IResult> Register([FromBody] RegisterUserDto request)
    {
        var serviceResponse = await _userService.Register(request);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }
    */

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginAdminDto request)
    {
        var serviceResponse = await _adminService.Login(request.Login, request.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "AdminController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        Response.Cookies.Append("my-cookie", serviceResponse.Data!, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { jwtToken = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IResult> GetInfoById(int adminId)
    {
        var serviceResponse = await _adminService.GetById(adminId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "AdminController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IResult> UpdateInfo(int adminId, string nickname)
    {
        var serviceResponse = await _adminService.UpdateInfo(adminId, nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "AdminController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }
}
