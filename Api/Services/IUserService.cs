using Api.Controllers;

namespace Api.Services;

public interface IUserService
{
    Task<UserResponse> GetById(int id);
    Task<UserResponse> Create(CreateUserRequest request);
}

public record CreateUserRequest(
    string Name,
    string Email
);

public record UpdateUserRequest(
    string? Name,
    string? Email
); 