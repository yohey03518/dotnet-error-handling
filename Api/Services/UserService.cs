using Api.Controllers;
using Api.Repositories;

namespace Api.Services;

public class UserService(
    IUserProfileRepository userProfileRepository,
    IUserPaymentTransactionRepository paymentTransactionRepository)
    : IUserService
{
    public async Task<UserResponse> GetById(int id)
    {
        var userProfile = await userProfileRepository.GetById(id);
        var paymentTransactions = await paymentTransactionRepository.GetByUserId(id);
        return new UserResponse(userProfile, paymentTransactions);
    }

    public async Task<UserResponse> Create(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }
}