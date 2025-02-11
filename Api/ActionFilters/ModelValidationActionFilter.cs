using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class ModelValidationActionFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = BaseResponse.ValidateFail(string.Join(",", errors));
            context.Result = new OkObjectResult(response);
            return Task.CompletedTask;
        }

        return next();
    }
} 