using MongoDB.Driver;
using MusicSitePrimeBackend.Domain.Models;

namespace MusicSitePrimeBackend.Domain.Repositories;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> GetBySecretOrDefaultAsync(string secret, CancellationToken ct);
}

public class UsersRepository : IUsersRepository
{
    private readonly IMongoCollection<User> _collection;

    public UsersRepository(IMongoCollection<User> collection)
    {
        _collection = collection;
    }

    public async Task<User?> GetBySecretOrDefaultAsync(string secret, CancellationToken ct)
    {
        return await _collection.Find(user => user.Secret == secret).FirstOrDefaultAsync(ct);
    }
}