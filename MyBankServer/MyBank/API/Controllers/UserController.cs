using Microsoft.AspNetCore.Mvc;
using MyBank.API.DataTransferObjects;
using MyBank.Application.Interfaces;

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
        var id = await _userService.Register(request.Email, request.Password, request.Nickname,
            request.Name, request.Surname, request.PassportSeries, request.PassportNumber);

        return Results.Json(new { userId = id});
    }

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginUserDto request)
    {
        var token = await _userService.Login(request.Email, request.Password);

        Response.Cookies.Append("my-cookie", token, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { jwtToken = token });
    }

    [HttpPut]
    public async Task<IResult> UpdateAccountInfo([FromBody] UserDto request)
    {
        await _userService.UpdatePersonalInfo(request.Id, request.Nickname, request.Name, request.Surname, request.Patronymic, request.PhoneNumber, request.PassportSeries, request.PassportNumber, request.Citizenship);
        return Results.Ok();
    }

    [HttpPut]
    public async Task<IResult> GetUserInfo(int userId)
    {
        await _userService

        return Results.Ok();
    }
}
