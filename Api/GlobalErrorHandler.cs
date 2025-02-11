using System.Net;
using System.Text.Json;
using Api;
using Microsoft.Data.SqlClient;

public class GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (SqlException ex)
        {
            logger.LogCritical(ex, "DB Error");
            // inform support team or switch to UM...
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new BaseResponse
            {
                IsSuccess = false,
                Message = "Unexpected Error Occurred"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response)); 
        }
    }
}