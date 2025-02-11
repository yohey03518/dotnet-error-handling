using System.ComponentModel;
using Grpc.Core;
using gRPC;

namespace gRPC.Services;

public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        throw new NotImplementedException("111");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}