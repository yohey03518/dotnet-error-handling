namespace Api.Domain;

public class PaymentTransaction
{
    public required string TransactionId { get; set; }
    public required decimal Amount { get; set; }
}