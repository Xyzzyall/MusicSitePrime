using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MusicSitePrimeBackend.Domain.Models;

public class Article
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Codename { get; set; }
    public string Name { get; set; }
    public string ShortText { get; set; }
    public string Text { get; set; }
    public List<string> Tags { get; set; }
    public Status ArticleStatus { get; set; } = Status.Draft;
    
    public Guid RelatedReleaseId { get; set; }


    public enum Status
    {
        Draft,
        Published,
    }
    
    public const string CollectionName = "Articles";
    public static readonly ICollection<CreateIndexModel<Article>> IndexModels = new[]{
        new CreateIndexModel<Article>(
            "{Codename : 1}",
            new CreateIndexOptions {Unique = true}
        ),
        new CreateIndexModel<Article>(
            "{Tags : 1}"
        )
    };
}