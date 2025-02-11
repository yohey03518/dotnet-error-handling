using Api.Controllers;

namespace Api.Services;

public class ShippingService : IShippingService
{
    public Task ShipOrder(int userId, string address, List<ProductRequest> products)
    {
        throw new PlaceOrderException(PlaceOrderError.ShippingNotAvailable, "Shipping service is not available");
    }
}