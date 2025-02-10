using Api.Domain;

namespace Api.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    public async Task<UserProfile> GetById(int id)
    {
        return new UserProfile { Id = id, Email = $"user{id}@example.com" };
    }
}