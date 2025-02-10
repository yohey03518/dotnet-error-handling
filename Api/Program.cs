using Api.ActionFilters;
using Api.Controllers;
using Api.Repositories;
using Api.Services;
using Castle.DynamicProxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddScoped<UserApiExceptionFilterAttribute>();

// Register repositories
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserPaymentTransactionRepository, UserPaymentTransactionRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();

// Register the proxy generator
// builder.Services.AddSingleton<ProxyGenerator>();

// Register the service with interception
// builder.Services.AddScoped<IUserService>(provider =>
// {
//     var proxyGenerator = provider.GetRequiredService<ProxyGenerator>();
//     var interceptor = provider.GetRequiredService<IAsyncInterceptor>();
//     var logger = provider.GetRequiredService<ILogger<UserService>>();
//     
//     var target = new UserService(logger);
//     return proxyGenerator.CreateInterfaceProxyWithTarget<IUserService>(target, interceptor);
// });
var app = builder.Build();

app.UseMiddleware<GlobalErrorHandler>();
app.MapControllers();

app.Run();