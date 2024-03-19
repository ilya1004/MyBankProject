using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces;
using MyBank.Domain.DataTransferObjects;
using MyBank.Domain.DataTransferObjects.CurrencyDtos;

namespace MyBank.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IResult> Add([FromBody] CurrencyDto currencyDto)
    {
        var serviceResponse = await _currencyService.Add(currencyDto);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoById(int currencyId)
    {
        var serviceResponse = await _currencyService.GetById(currencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetInfoByCode(string currencyCode)
    {
        var serviceResponse = await _currencyService.GetByCode(currencyCode);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    public async Task<IResult> GetAllInfo()
    {
        var serviceResponse = await _currencyService.GetAll();

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = "Admin")]
    public async Task<IResult> Delete(int currencyId)
    {
        var serviceResponse = await _currencyService.Delete(currencyId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(new ErrorDto
            {
                ControllerName = "CurrencyController",
                Message = serviceResponse.Message,
            },
            statusCode: 400);
        }

        return Results.Json(new { userId = serviceResponse.Data }, statusCode: 200);
    }
}