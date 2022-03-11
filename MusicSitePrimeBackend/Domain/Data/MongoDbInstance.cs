using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicSitePrimeBackend.Configurations;
using MusicSitePrimeBackend.Domain.Models;

namespace MusicSitePrimeBackend.Domain.Data;

public class MongoDbInstance
{
    private readonly MongoDbConfiguration _configuration;
    
    public MongoDbInstance(IOptions<MongoDbConfiguration> mongoDbSettings)
    {
        _configuration = mongoDbSettings.Value;

        var (client, db) = InstantiateClientAndDb();
        
        ConfigureCollections(db);
    }

    private static void ConfigureCollections(IMongoDatabase db)
    {
        var articles = db.GetCollection<Article>(Article.CollectionName);
        articles.Indexes.CreateMany(Article.IndexModels);

        var releases = db.GetCollection<Release>(Release.CollectionName);
        releases.Indexes.CreateMany(Release.IndexModels);

        var users = db.GetCollection<User>(User.CollectionName);
        users.Indexes.CreateMany(User.IndexModels);
    }

    public (MongoClient client, IMongoDatabase database) InstantiateClientAndDb()
    {
        var client = new MongoClient(
            _configuration.ConnectionString
        );
        var database = client.GetDatabase(
            _configuration.DatabaseName
        );   
        return (client, database);
    }
}