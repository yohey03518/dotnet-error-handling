namespace Api.Handlers;

public class RetryHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var retryCount = 0;
        const int maxRetries = 3;
        const int delay = 100;
        HttpResponseMessage ret = null;

        while (retryCount <= maxRetries)
        {
            ret = await base.SendAsync(request, cancellationToken);
            if (ret.StatusCode != System.Net.HttpStatusCode.InternalServerError)
            {
                return ret;
            }
            
            retryCount++;
            await Task.Delay(delay * retryCount, cancellationToken);
        }

        return ret;
    }
}