using Api.Domain;

namespace Api.Repositories;

public class UserPaymentTransactionRepository : IUserPaymentTransactionRepository
{
    public async Task<List<PaymentTransaction>> GetByUserId(int id)
    {
        return
        [
            new PaymentTransaction() { TransactionId = "txn1", Amount = 100.00m },
            new PaymentTransaction() { TransactionId = "txn2", Amount = 150.50m }
        ];
    }
}