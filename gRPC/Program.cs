using gRPC.Services;
using gRPC.Interceptors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<GlobalErrorHandleInterceptor>();
}).AddServiceOptions<GreeterService>(options =>
{
    options.Interceptors.Add<PlaceOrderErrorHandleInterceptor>();
});;

var app = builder.Build();

app.MapGrpcService<GreeterService>();

app.Run();