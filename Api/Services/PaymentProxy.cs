using Api.Controllers;

namespace Api.Services;

public class PaymentProxy : IPaymentProxy
{
    public Task ProcessPayment(int userId, string paymentMethod, decimal totalAmount)
    {
        throw new PlaceOrderException(PlaceOrderError.PaymentFail, "payment fail message from payment provider");
    }
}