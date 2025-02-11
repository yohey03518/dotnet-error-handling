using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Api.ActionFilters;
using Api.Services;

namespace Api.Controllers;

[Route("api/[controller]")]
public class OrderController(OrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<BaseResponse> PlaceOrder([FromBody] PlaceOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BaseResponse.ValidateFail(string.Join(",", errors));
        }

        // do something
        return BaseResponse.Success();
    }

    [HttpPost("action-filter")]
    [ServiceFilter(typeof(ModelValidationFilterAttribute))]
    public async Task<BaseResponse> PlaceOrderWithActionFilter([FromBody] PlaceOrderRequest request)
    {
        // do something
        return BaseResponse.Success();
    }

    [HttpPost("exception-filter")]
    [ValidationFailFilter]
    [ServiceFilter(typeof(PlaceOrderExceptionFilterAttribute))]
    public async Task<BaseResponse> PlaceOrderWithExceptionFilter([FromBody] PlaceOrderRequest request)
    {
        await orderService.PlaceOrder(request);
        return BaseResponse.Success();
    }

    [HttpPost("fail-handle")]
    public async Task<BaseResponse> PlaceOrderFailHandle([FromBody] PlaceOrderRequest request)
    {
        await orderService.PlaceOrderWithFailHandle1(request);
        return BaseResponse.Success();
    }
    
    [HttpPost("fail-handle")]
    public async Task<BaseResponse> PlaceOrderFailHandleResultPattern([FromBody] PlaceOrderRequest request)
    {
        await orderService.PlaceOrderWithFailHandleResultPattern(request);
        return BaseResponse.Success();
    }
}

public enum PlaceOrderError
{
    Unknown = 0,
    PaymentFail = 1,
    ProductSoldOut = 2,
    ShippingNotAvailable = 3
}

public class PlaceOrderException(PlaceOrderError errorType, string message) : Exception(message)
{
    private readonly PlaceOrderError _errorType = errorType;
}

public class ProductRequest
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}

public class PlaceOrderRequest
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string PaymentMethod { get; set; } = null!;

    [Required(ErrorMessage = "{0} is required")]
    [MaxLength(10, ErrorMessage = "{0} must not exceed {1} characters")]
    public string Address { get; set; } = null!;

    public List<ProductRequest> Products { get; set; } = [];
}