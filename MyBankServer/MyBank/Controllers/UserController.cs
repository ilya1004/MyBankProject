using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.UserDtos;
using MyBank.Application.Utils;
using Newtonsoft.Json;

namespace MyBank.API.Controllers;


[EnableCors(PolicyName = "_myAllowSpecificOrigins")]
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
    public async Task<IResult> Register([FromBody] RegisterUserDto dto)
    {
        var serviceResponse = await _userService.Register(dto.Email, dto.Password, dto.Nickname,
            dto.Name, dto.Surname, dto.Patronymic, dto.PassportSeries, dto.PassportNumber, dto.Citizenship);
        
        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
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

        Response.Cookies.Append("my-cookie", serviceResponse.Data.Item2, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { id = serviceResponse.Data.Item1 }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int userId, bool includeData)
    {
        var serviceResponse = await _userService.GetById(userId, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
                statusCode: 400);
        }

        return Results.Json(new { item = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(serviceResponse.Data, settings: new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
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

        return Results.Json(new { list = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateAccountInfo([FromBody] UpdateUserAccountInfoDto dto)
    {
        var serviceResponse = await _userService.UpdateAccountInfo(dto.Id, dto.Email, dto.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdatePersonalInfo([FromBody] UpdateUserPersonalInfoDto dto)
    {
        var serviceResponse = await _userService.UpdatePersonalInfo(dto.Id, dto.Nickname, dto.Name, 
            dto.Surname, dto.Patronymic, dto.PhoneNumber, dto.PassportSeries, dto.PassportNumber, dto.Citizenship);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
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
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.UserAndAdminPolicy)]
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
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }
}
