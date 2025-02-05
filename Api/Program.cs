var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<GlobalErrorHandler>();
app.MapGet("/", () =>
{
    throw new Exception("123");
});

app.Run();