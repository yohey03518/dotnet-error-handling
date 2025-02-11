using Api.Controllers;
using Api.Interceptors;

namespace Api.Services;

public interface IUserService
{
    Task<UserResponse> GetById(int id);
    Task<UserResponse> GetByIdWithDefault(int id);
    
    [Retry(MaxAttempts = 3)]
    Task<UserResponse> GetByIdWithRetry(int id);
} 