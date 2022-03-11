using MongoDB.Driver;
using MusicSitePrimeBackend.Domain.Data;
using MusicSitePrimeBackend.Domain.Models;
using MusicSitePrimeBackend.Domain.Repositories;

namespace MusicSitePrimeBackend.Domain;

public class UnitOfWork
{
    public readonly IArticlesRepository Articles;
    public readonly IReleasesRepository Releases;
    public readonly IUsersRepository Users;

    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;

    public UnitOfWork(MongoDbInstance instance)
    {
        (_mongoClient, _mongoDatabase) = instance.InstantiateClientAndDb();
        Articles = new ArticlesRepository(
            _mongoDatabase.GetCollection<Article>(Article.CollectionName)
        );
        Releases = new ReleasesRepository(
            _mongoDatabase.GetCollection<Release>(Release.CollectionName)
        );
        Users = new UsersRepository(
            _mongoDatabase.GetCollection<User>(User.CollectionName)
        );
    }

    public Task<IClientSessionHandle> Session(CancellationToken ct)
    {
        return _mongoClient.StartSessionAsync(cancellationToken: ct);
    }
}