using Microsoft.AspNetCore.Http;
using MyBank.API.DataTransferObjects.UserDtos;
using MyBank.Application.Utils;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICookieValidator _cookieValidator;
    public UserController(IUserService userService, ICookieValidator cookieValidator)
    {
        _userService = userService;
        _cookieValidator = cookieValidator;
    }

    [HttpPost]
    public async Task<IResult> Register([FromBody] RegisterUserDto dto)
    {
        var serviceResponse = await _userService.Register(dto.Email, dto.Password, dto.Nickname,
            dto.Name, dto.Surname, dto.Patronymic, dto.PhoneNumber, dto.PassportSeries, dto.PassportNumber, dto.Citizenship);
        
        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 201);
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
            statusCode: 401); 
        }

        Response.Cookies.Append("my-cookie", serviceResponse.Data.Item2, new CookieOptions { SameSite = SameSiteMode.Lax });

        return Results.Json(new { id = serviceResponse.Data.Item1 }, statusCode: 200);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public IResult Logout()
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        Response.Cookies.Append("my-cookie", "", new CookieOptions { SameSite = SameSiteMode.Lax, Expires = DateTime.Now.AddDays(-1) });

        return Results.Json(new { status = true }, statusCode: 200);
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UploadAvatarFile(IFormFile file)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.UploadAvatarFile(file, id!.Value);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetInfoById(int userId, bool includeData)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

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

        return Results.Json(new { item = MyJsonConverter<User>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetInfoCurrent(bool includeData)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.GetById(id!.Value, includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(new { item = MyJsonConverter<User>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> GetAvatarCurrent()
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.GetAvatarImagePath(id!.Value);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.File(serviceResponse.Data!, $"image/{Path.GetExtension(serviceResponse.Data)![1..]}");
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.ModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfo(bool includeData)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse  = await _userService.GetAll(includeData);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = serviceResponse.Message
            },
            statusCode: 400);
        }

        return Results.Json(new { list = MyJsonConverter<List<User>>.Convert(serviceResponse.Data) }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdatePassword([FromBody] UpdateUserPasswordDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.UpdatePassword(id!.Value, dto.OldEmail, dto.OldPassword, dto.NewPassword);

        if (serviceResponse.Status == false)
        {
            if (serviceResponse.Message == "Неверный пароль")
            {
                return Results.Json(new ErrorDto
                {
                    ControllerName = "UsersController",
                    Message = serviceResponse.Message
                },
                statusCode: 401);
            } 
            else
            {
                return Results.Json(new ErrorDto
                {
                    ControllerName = "UsersController",
                    Message = serviceResponse.Message
                },
                statusCode: 400);
            }
            
        }
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdateEmail([FromBody] UpdateUserEmailDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.UpdateEmail(id!.Value, dto.OldEmail, dto.OldPassword, dto.NewEmail);

        if (serviceResponse.Status == false)
        {
            if (serviceResponse.Message == "Неверный пароль")
            {
                return Results.Json(new ErrorDto
                {
                    ControllerName = "UsersController",
                    Message = serviceResponse.Message
                },
                statusCode: 401);
            }
            else
            {
                return Results.Json(new ErrorDto
                {
                    ControllerName = "UsersController",
                    Message = serviceResponse.Message
                },
                statusCode: 400);
            }

        }
        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> UpdatePersonalInfoCurr([FromBody] UpdateUserPersonalInfoDto dto)
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.UpdatePersonalInfo(id!.Value, dto.Nickname, dto.Name, 
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
    public async Task<IResult> Delete()
    {
        var (status, message, errorCode, role, id) = _cookieValidator.HandleCookie(Request.Headers.Cookie[0]!);

        if (status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "UsersController",
                Message = message!
            },
            statusCode: 400);
        }

        var serviceResponse = await _userService.Delete(id!.Value);

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
