using System.Collections.ObjectModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MusicSitePrimeBackend.Domain.Models;

public class Release
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Codename { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ReleaseType { get; set; }
    public List<ReleaseSong> Songs { get; set; }
    
    public class ReleaseSong
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Lyrics { get; set; }
        public int LengthSecs { get; set; }
    }

    public const string CollectionName = "Releases";
    public static readonly ICollection<CreateIndexModel<Release>> IndexModels = new[]
    {
        new CreateIndexModel<Release>(
            "{Codename : 1}",
            new CreateIndexOptions {Unique = true}
        )
    };
}