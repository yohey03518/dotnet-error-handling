using System.Reflection;
using Castle.DynamicProxy;

namespace Api.Interceptors;

public class RetryInterceptor(ILogger<RetryInterceptor> logger) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var methodInfo = invocation.Method;
        var retryAttribute = methodInfo.GetCustomAttribute<RetryAttribute>();

        if (retryAttribute == null)
        {
            // No retry attribute, just proceed with the invocation
            invocation.Proceed();
            return;
        }

        if (typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
        {
            InterceptAsync(invocation, retryAttribute).GetAwaiter().GetResult();
        }
        else
        {
            InterceptSync(invocation, retryAttribute);
        }
    }

    private async Task InterceptAsync(IInvocation invocation, RetryAttribute retryAttribute)
    {
        var attempts = 0;

        while (true)
        {
            try
            {
                attempts++;
                invocation.Proceed();
                
                if (invocation.ReturnValue is Task returnValueTask)
                {
                    await returnValueTask;
                }
                
                return;
            }
            catch (Exception ex) when (attempts < retryAttribute.MaxAttempts)
            {
                logger.LogWarning(ex,
                    "Attempt {Attempt} of {MaxAttempts} failed for method {MethodName}.",
                    attempts, retryAttribute.MaxAttempts, invocation.Method.Name);
            }
        }
    }

    private void InterceptSync(IInvocation invocation, RetryAttribute retryAttribute)
    {
        var attempts = 0;

        while (true)
        {
            try
            {
                attempts++;
                invocation.Proceed();
                return;
            }
            catch (Exception ex) when (attempts < retryAttribute.MaxAttempts)
            {
                logger.LogWarning(ex,
                    "Attempt {Attempt} of {MaxAttempts} failed for method {MethodName}.",
                    attempts, retryAttribute.MaxAttempts, invocation.Method.Name);

            }
        }
    }
} 