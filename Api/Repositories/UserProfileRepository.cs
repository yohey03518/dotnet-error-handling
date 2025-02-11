using Api.Domain;
using Microsoft.Data.SqlClient;

namespace Api.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    public async Task<UserProfile> GetById(int id)
    {
        
        return new UserProfile { Id = id, Email = $"user{id}@example.com" };
    }
}

