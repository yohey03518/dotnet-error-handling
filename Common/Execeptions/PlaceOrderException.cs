using Common.Domain;

namespace Common.Execeptions;

public class PlaceOrderException(PlaceOrderError errorType, string message) : Exception(message)
{
    public readonly PlaceOrderError ErrorType = errorType;
}