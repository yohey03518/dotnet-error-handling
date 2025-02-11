using Api.ActionFilters;
using Api.Controllers;
using Api.Repositories;
using Api.Services;
using Api.Interceptors;
using Castle.DynamicProxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<UserApiExceptionFilterAttribute>();
builder.Services.AddScoped<ModelValidationActionFilter>();

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserPaymentTransactionRepository, UserPaymentTransactionRepository>();

// Register the interceptor
builder.Services.AddSingleton<IInterceptor, RetryInterceptor>();

// Register UserService with interceptor
builder.Services.AddScoped(serviceProvider =>
{
    var proxyGenerator = new ProxyGenerator();
    var userProfileRepo = serviceProvider.GetRequiredService<IUserProfileRepository>();
    var paymentTransactionRepo = serviceProvider.GetRequiredService<IUserPaymentTransactionRepository>();
    var logger = serviceProvider.GetRequiredService<ILogger<UserService>>();
    var interceptor = serviceProvider.GetRequiredService<IInterceptor>();

    var target = new UserService(userProfileRepo, paymentTransactionRepo, logger);
    return proxyGenerator.CreateInterfaceProxyWithTarget<IUserService>(target, interceptor);
});

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandler>();
app.MapControllers();

app.Run();