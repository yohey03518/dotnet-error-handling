using Api.Controllers;
using Api.Exceptions;
using Api.Services;

namespace Api.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<int> CreateOrder(PlaceOrderRequest request)
    {
        throw new ValidationFailException("Exceed daily limit");
        // throw new PlaceOrderException(PlaceOrderError.ProductSoldOut, "Product is sold out");
    }

    public void UpdateOrderStatus(int orderId, OrderStatus orderStatus)
    {
        throw new NotImplementedException();
    }
}