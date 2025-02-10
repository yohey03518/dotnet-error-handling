using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class UserApiExceptionFilterAttribute(ILogger<UserApiExceptionFilterAttribute> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "An error occurred in UserController");

        var response = new BaseResponse
        {
            IsSuccess = false,
            Message = context.Exception switch
            {
                ArgumentException => "Invalid input provided",
                KeyNotFoundException => "Requested resource not found",
                _ => "An unexpected error occurred"
            }
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = context.Exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            }
        };

        context.ExceptionHandled = true;
    }
}