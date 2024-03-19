using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;
using MyBank.Persistence.Interfaces;
using MyBank.Domain.DataTransferObjects.UserDtos;
using MyBank.Domain.DataTransferObjects;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

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

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginUserDto request)
    {
        var serviceResponse = await _userService.Login(request.Email, request.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto 
            { 
                ControllerName = "UsersController", 
                Message = serviceResponse.Message
            },
            statusCode: 400); 
        }

        Response.Cookies.Append("my-cookie", serviceResponse.Data!, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { jwtToken = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = "UserPolicy")]         // как сделать для модератора тоже??
    public async Task<IResult> GetInfoById(int userId)
    {
        var serviceResponse = await _userService.GetById(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
                statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = "UserAndModeratorPolicy")]
    public async Task<IResult> GetAllInfo()
    {
        var serviceResponse  = await _userService.GetAll();

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpPut]
    [Authorize]
    public async Task<IResult> UpdateAccountInfo([FromBody] UserDto request)
    {
        var serviceResponse = await _userService.UpdateAccountInfo(request.Id, request.Email, request.HashedPassword); // this password is not hashed!

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpPut]
    [Authorize]
    public async Task<IResult> UpdatePersonalInfo([FromBody] UserDto request)
    {
        var serviceResponse = await _userService.UpdatePersonalInfo(request.Id, request.Nickname, request.Name, request.Surname, request.Patronymic, request.PhoneNumber, request.PassportSeries, request.PassportNumber, request.Citizenship);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpPut]
    [Authorize]  // для админа
    public async Task<IResult> UpdateStatus(int userId, bool isActive)
    {
        var serviceResponse = await _userService.UpdateStatus(userId, isActive);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IResult> Delete(int userId)
    {
        var serviceResponse = await _userService.Delete(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(serviceResponse.Data!, statusCode: 200);
    }
}
