namespace Common.Domain;

public enum PlaceOrderError
{
    Unknown = 0,
    PaymentFail = 1,
    ProductSoldOut = 2,
    ShippingNotAvailable = 3
}