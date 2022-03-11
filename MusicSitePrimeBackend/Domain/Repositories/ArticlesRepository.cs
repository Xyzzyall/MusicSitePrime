using MongoDB.Driver;
using MusicSitePrimeBackend.Domain.Models;

namespace MusicSitePrimeBackend.Domain.Repositories;

public interface IArticlesRepository : IRepository<Article>
{
    Task<IEnumerable<Article>> GetAllPagedAsync(int skip, int take, CancellationToken ct);
    Task<IEnumerable<Article>> GetPublishedPagedAsync(int skip, int take, CancellationToken ct);
}

public class ArticlesRepository : IArticlesRepository
{
    private readonly IMongoCollection<Article> _collection;
    public ArticlesRepository(IMongoCollection<Article> collection)
    {
        _collection = collection;
    }

    public async Task<IEnumerable<Article>> GetAllPagedAsync(int skip, int take, CancellationToken ct)
    {
        return await _collection.Find(_=>true).Skip(skip).Limit(take).ToListAsync(cancellationToken: ct);
    }

    public async Task<IEnumerable<Article>> GetPublishedPagedAsync(int skip, int take, CancellationToken ct)
    {
        return await _collection.Find(article => article.ArticleStatus == Article.Status.Published)
            .Skip(skip)
            .Limit(take)
            .ToListAsync(cancellationToken: ct);
    }
}