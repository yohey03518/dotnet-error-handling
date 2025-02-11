using System.Net;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class PlaceOrderExceptionFilterAttribute(ILogger<PlaceOrderExceptionFilterAttribute> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "An error occurred during place order");

        if (context.Exception is PlaceOrderException exception)
        {
            context.Result = new ObjectResult(new BaseResponse
            {
                IsSuccess = false,
                Message = exception.ErrorType switch
                {
                    PlaceOrderError.PaymentFail => "Payment failed",
                    PlaceOrderError.ProductSoldOut => "Product is sold out",
                    PlaceOrderError.ShippingNotAvailable => "Shipping is not available",
                    _ => "An unexpected error occurred while placing the order"
                }
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            context.ExceptionHandled = true;
        }

        base.OnException(context);
    }
}