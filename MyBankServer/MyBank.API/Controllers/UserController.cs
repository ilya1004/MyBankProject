using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Services;
using MyBank.API.Records;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IResult> Register([FromBody] RegisterUserRequest request)
    {
        await _userService.Register(request.Email, request.Password, request.Nickname, 
            request.Name, request.Surname, request.PassportSeries, request.PassportNumber);

        return Results.Ok();
    }

    [HttpPost]
    public async Task<IResult> Login(LoginUserRequest request)
    {
        var token = await _userService.Login(request.Email, request.Password);

        return Results.Ok(token);
    }
}
