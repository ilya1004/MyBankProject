using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Cors;
using MyBank.API.DataTransferObjects.CreditRequestDtos;

namespace MyBank.API.Controllers;

[ApiController]
[EnableCors(PolicyName = "myCorsPolicy")]
[Route("api/[controller]/[action]")]
public class CreditRequestsController : ControllerBase
{
    private readonly ICreditRequestsService _creditRequestsService;
    private readonly IMapper _mapper;

    public CreditRequestsController(ICreditRequestsService creditRequestsService, IMapper mapper)
    {
        _creditRequestsService = creditRequestsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.UserPolicy)]
    public async Task<IResult> Add([FromBody] CreditRequestDto dto)
    {
        var serviceResponse = await _creditRequestsService.Add(_mapper.Map<CreditRequest>(dto));

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditRequestsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { id = serviceResponse.Data }, statusCode: 200);
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorAndAdminPolicy)]
    public async Task<IResult> GetAllInfoByUserId(int userId)
    {
        var serviceResponse = await _creditRequestsService.GetAllByUser(userId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditRequestsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { list = serviceResponse.Data }, statusCode: 200);
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.ModeratorPolicy)]
    public async Task<IResult> UpdateStatus(int creditRequestId, int moderatorId, bool isApproved)
    {
        var serviceResponse = await _creditRequestsService.UpdateIsApproved(
            creditRequestId,
            moderatorId,
            isApproved
        );

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CreditRequestsController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }

    [HttpDelete]
    [Authorize(Policy = AuthorizationPolicies.UserAndModeratorPolicy)]
    public async Task<IResult> Delete(int creditRequestId)
    {
        var serviceResponse = await _creditRequestsService.Delete(creditRequestId);

        if (serviceResponse.Status == false)
        {
            return Results.Json(
                new ErrorDto
                {
                    ControllerName = "CurrencyController",
                    Message = serviceResponse.Message,
                },
                statusCode: 400
            );
        }

        return Results.Json(new { status = serviceResponse.Data }, statusCode: 200);
    }
}
