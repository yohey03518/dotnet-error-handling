using Api.Common;

namespace Api.Services;

public interface IPaymentProxy
{
    Task ProcessPayment(int userId, string paymentMethod, decimal totalAmount);
    Task<Result> ProcessPaymentResult(int userId, string paymentMethod, decimal totalAmount);
}