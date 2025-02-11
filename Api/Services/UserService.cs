using Api.Controllers;
using Api.Domain;
using Api.Repositories;
using Api.Interceptors;

namespace Api.Services;

public class UserService(
    IUserProfileRepository userProfileRepository,
    IUserPaymentTransactionRepository paymentTransactionRepository,
    ILogger<UserService> logger): IUserService
{
    public async Task<UserResponse> GetById(int id)
    {
        var userProfile = await userProfileRepository.GetById(id);
        var paymentTransactions = await paymentTransactionRepository.GetByUserId(id);
        return new UserResponse(userProfile, paymentTransactions);
    }
    
    public async Task<UserResponse> GetByIdWithDefault(int id)
    {
        var userProfile = await userProfileRepository.GetById(id);

        List<PaymentTransaction> paymentTransactions;

        try
        {
            paymentTransactions = await paymentTransactionRepository.GetByUserId(id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting payment transactions for user {UserId}", id);
            paymentTransactions = [];
        }
        return new UserResponse(userProfile, paymentTransactions);
    }

    public async Task<UserResponse> GetByIdWithRetry(int id)
    {
        throw new Exception("123");
        var userProfile = await userProfileRepository.GetById(id);
        var paymentTransactions = await paymentTransactionRepository.GetByUserId(id);
        return new UserResponse(userProfile, paymentTransactions);
    } 

    // public async Task<UserResponse> Create(CreateUserRequest request)
    // {
    //     throw new NotImplementedException();
    // }
}