using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Api.ActionFilters;

namespace Api.Controllers;

[Route("api/[controller]")]
public class OrderController : ControllerBase
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
    [ServiceFilter(typeof(ModelValidationActionFilter))]
    public async Task<BaseResponse> PlaceOrderWithActionFilter([FromBody] PlaceOrderRequest request)
    {
        // do something
        return BaseResponse.Success();
    }
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