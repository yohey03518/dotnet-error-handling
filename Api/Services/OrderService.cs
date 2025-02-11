using Api.Controllers;
using Api.Repositories;
using Common.Domain;
using Common.Execeptions;

namespace Api.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IPaymentProxy paymentProxy,
    IShippingService shippingService)
{
    public async Task PlaceOrder(PlaceOrderRequest request)
    {
        var totalAmount = request.Products.Sum(p => p.Price * p.Amount);

        await orderRepository.CreateOrder(request);
        await paymentProxy.ProcessPayment(request.UserId, request.PaymentMethod, totalAmount);
        await shippingService.ShipOrder(request.UserId, request.Address, request.Products);
    }
    
    public async Task PlaceOrderWithFailHandle1(PlaceOrderRequest request)
    {
        var totalAmount = request.Products.Sum(p => p.Price * p.Amount);

        var orderId = await orderRepository.CreateOrder(request);

        try
        {
            await paymentProxy.ProcessPayment(request.UserId, request.PaymentMethod, totalAmount);
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.PaymentSucceeded);
        }
        catch (Exception)
        {
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.PaymentFailed);
            // throw e; // should not throw by this way to prevent stack trace missing
            // if the exception has been thrown, no need to log here
            throw;
        }

        try
        {
            await shippingService.ShipOrder(request.UserId, request.Address, request.Products);
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.Shipping);
        }
        catch (Exception)
        {
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.ShippingRequestFail);
            throw;
        }
    }
    
    public async Task PlaceOrderWithFailHandleResultPattern(PlaceOrderRequest request)
    {
        var totalAmount = request.Products.Sum(p => p.Price * p.Amount);

        var orderId = await orderRepository.CreateOrder(request);

        var paymentResult = await paymentProxy.ProcessPaymentResult(orderId, request.PaymentMethod, totalAmount);
        if (!paymentResult.IsSuccess)
        {
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.PaymentFailed);
            throw new PlaceOrderException(PlaceOrderError.PaymentFail, paymentResult.ErrorMessage);
        }
        orderRepository.UpdateOrderStatus(orderId, OrderStatus.PaymentSucceeded);

        var shippingResult =  await shippingService.ShipOrderResult(request.UserId, request.Address, request.Products);
        if (!shippingResult.IsSuccess)
        {
            orderRepository.UpdateOrderStatus(orderId, OrderStatus.ShippingRequestFail);
            throw new PlaceOrderException(PlaceOrderError.ShippingNotAvailable, shippingResult.ErrorMessage);
        }
        orderRepository.UpdateOrderStatus(orderId, OrderStatus.Shipping);
    }
}

public enum OrderStatus
{
    Unknown = 0,
    New = 1,
    Completed = 2,
    PaymentFailed = 3,
    PaymentSucceeded = 4,
    ShippingRequestFail = 5,
    Shipping = 6,
}