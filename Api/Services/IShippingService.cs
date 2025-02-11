using Api.Controllers;

namespace Api.Services;

public interface IShippingService
{
    Task ShipOrder(int userId, string address, List<ProductRequest> products);
}