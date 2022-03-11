using MongoDB.Bson;
using MongoDB.Driver;
using MusicSitePrimeBackend.Domain.Models;

namespace MusicSitePrimeBackend.Domain.Repositories;

public interface IReleasesRepository : IRepository<Release>
{
    Task<Release> GetByIdAsync(ObjectId id, CancellationToken ct);
}

public class ReleasesRepository : IReleasesRepository
{
    private readonly IMongoCollection<Release> _collection;

    public ReleasesRepository(IMongoCollection<Release> collection)
    {
        _collection = collection;
    }

    public async Task<Release> GetByIdAsync(ObjectId id, CancellationToken ct)
    {
        return await _collection.Find(release => release.Id == id).FirstAsync(ct);
    }
}