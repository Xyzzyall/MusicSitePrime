using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MusicSitePrimeBackend.Domain.Models;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string UserName { get; set; }
    public string Secret { get; set; }
    public List<string> Rights { get; set; }
    
    public const string CollectionName = "Users";
    public static readonly ICollection<CreateIndexModel<User>> IndexModels = new[]{
        new CreateIndexModel<User>(
            "{UserName : 1}",
            new CreateIndexOptions {Unique = true}
        ),
        new CreateIndexModel<User>(
            "{Secret : 1}",
            new CreateIndexOptions {Unique = true}
        ),
    };
}