using Api.Domain;

namespace Api.Repositories;

public interface IUserPaymentTransactionRepository
{
    Task<List<PaymentTransaction>> GetByUserId(int id);
}