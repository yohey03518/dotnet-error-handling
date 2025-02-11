namespace Api.Services;

public interface IPaymentProxy
{
    Task ProcessPayment(int userId, string paymentMethod, decimal totalAmount);
}