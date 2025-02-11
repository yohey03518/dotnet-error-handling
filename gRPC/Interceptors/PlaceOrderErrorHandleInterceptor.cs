using Grpc.Core;
using Grpc.Core.Interceptors;
using Common.Execeptions;

namespace gRPC.Interceptors;

public class PlaceOrderErrorHandleInterceptor(ILogger<PlaceOrderErrorHandleInterceptor> logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (PlaceOrderException e)
        {
            logger.LogWarning(e, "Place order fail");

            var trailer = new Metadata
            {
                {"error-code", ((int)e.ErrorType).ToString()},
                {"error-message", e.Message},
            };

            throw new RpcException(new Status(StatusCode.InvalidArgument, e.Message), trailer);
        }
    }
}