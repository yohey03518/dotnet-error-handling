using Api.Common;
using Api.Controllers;
using Common.Domain;
using Common.Execeptions;

namespace Api.Services;

public class PaymentProxy(HttpClient httpClient) : IPaymentProxy
{
    public Task ProcessPayment(int userId, string paymentMethod, decimal totalAmount)
    {
        throw new PlaceOrderException(PlaceOrderError.PaymentFail, "payment fail message from payment provider");
    }

    public async Task<Result> ProcessPaymentResult(int userId, string paymentMethod, decimal totalAmount)
    {
        try
        {
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("payment fail message from payment provider", e);
        }
    }
}