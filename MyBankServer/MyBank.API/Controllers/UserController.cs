using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Services;
using MyBank.API.Records;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Register(RegisterUserRequest request, UserService userService)
    {
        await userService.Register(request.Email, request.Password, request.Nickname, 
            request.Name, request.Surname, request.PassportSeries, request.PassportNumber);

        return Results.Ok();
    }

    [HttpPost]
    public async Task<IResult> Login(LoginUserRequest request, UserService userService)
    {
        var token = await userService.Login(request.Email, request.Password);

        return Results.Ok(token);
    }
}
