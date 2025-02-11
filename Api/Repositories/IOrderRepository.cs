using Api.Controllers;
using Api.Services;

namespace Api.Repositories;

public interface IOrderRepository
{
    Task<int> CreateOrder(PlaceOrderRequest request);
    void UpdateOrderStatus(int orderId, OrderStatus orderStatus);
}