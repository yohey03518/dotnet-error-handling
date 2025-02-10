using Microsoft.AspNetCore.Mvc;
using Api.ActionFilters;
using Api.Domain;
using Api.Services;

namespace Api.Controllers;

[ServiceFilter(typeof(UserApiExceptionFilterAttribute))]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<BaseResponse> GetUserById(int id)
    {
        var response = await userService.GetById(id);

        return BaseResponse<UserResponse>.Success(response);
    }

    // [HttpPost]
    // public async Task<ActionResult<BaseResponse>> CreateUser([FromBody] CreateUserRequest request)
    // {
    //     return Ok(BaseResponse.Success());
    // }
}

public class UserResponse
{
    public UserResponse(UserProfile userProfile, IEnumerable<PaymentTransaction> paymentTransactions)
    {
        Id = userProfile.Id;
        Email = userProfile.Email;
        TotalDepositAmount = paymentTransactions.Sum(x => x.Amount);
    }

    public int Id { get; set; }
    public string Email { get; set; }
    public decimal TotalDepositAmount { get; set; }
}