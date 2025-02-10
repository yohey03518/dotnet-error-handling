using Api.Domain;

namespace Api.Repositories;

public interface IUserProfileRepository
{
    public Task<UserProfile> GetById(int id);
}