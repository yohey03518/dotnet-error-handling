using System.Net;
using System.Text.Json;
using Api;

public class GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new BaseResponse
            {
                IsSuccess = false,
                Message = "Internal Server Error"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}