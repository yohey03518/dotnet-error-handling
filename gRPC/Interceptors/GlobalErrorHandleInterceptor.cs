using System.ComponentModel.DataAnnotations;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Diagnostics;
using gRPC.Exceptions;

namespace gRPC.Interceptors;

public class GlobalErrorHandleInterceptor(ILogger<GlobalErrorHandleInterceptor> logger) : Interceptor
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
        catch (ValidationFailException e)
        {
            logger.LogWarning(e, "Validation fail in gRPC request");
            throw new RpcException(new Status(StatusCode.InvalidArgument, e.Message));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error gRPC");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}