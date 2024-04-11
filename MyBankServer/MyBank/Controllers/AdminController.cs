using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.AdminDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost]
    public async Task<IResult> Register([FromBody] RegisterAdminDto dto)
    {
        var serviceResponse = await _adminService.Register(dto.Login, dto.Password, dto.Nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "AdminController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPost]
    public async Task<IResult> Login([FromBody] LoginAdminDto request)
    {
        var serviceResponse = await _adminService.Login(request.Login, request.Password);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "AdminController",
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

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> GetInfoById(int adminId)
    {
        var serviceResponse = await _adminService.GetById(adminId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "AdminController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        return Results.Json(new { item = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IResult> UpdateInfo(int adminId, string nickname)
    {
        var serviceResponse = await _adminService.UpdateInfo(adminId, nickname);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "AdminController",
                    Message = serviceResponse.Message
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data! }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.AdminPolicy)]
    public async Task<IResult> Delete(int adminId)
    {
        var serviceResponse = await _adminService.Delete(adminId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "AdminController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
