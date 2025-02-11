using Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class ValidationFailFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationFailException)
        {
            context.ExceptionHandled = true;
            context.Result = new BadRequestObjectResult(BaseResponse.ValidateFail(context.Exception.Message));
        }

        base.OnException(context);
    }
}