using Api.Common;
using Api.Controllers;

namespace Api.Services;

public class ShippingService : IShippingService
{
    public Task ShipOrder(int userId, string address, List<ProductRequest> products)
    {
        throw new PlaceOrderException(PlaceOrderError.ShippingNotAvailable, "Shipping service is not available");
    }

    public async Task<Result> ShipOrderResult(int userId, string address, List<ProductRequest> products)
    {
        try
        {
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure("Failed to ship order", e);
        }
    }
}