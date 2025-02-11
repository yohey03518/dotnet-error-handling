using Api.Domain;

namespace Api.Repositories;

public class UserPaymentTransactionDefaultValueDecorator(IUserPaymentTransactionRepository decoratee, ILogger<UserPaymentTransactionDefaultValueDecorator> logger) : IUserPaymentTransactionRepository
{
    public async Task<List<PaymentTransaction>> GetByUserId(int id)
    {
        try
        {
            return await decoratee.GetByUserId(id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting payment transactions for user {UserId}", id);
            return [];
        }
    }
}