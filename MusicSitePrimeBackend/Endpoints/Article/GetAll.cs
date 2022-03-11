using FastEndpoints;
using MusicSitePrimeBackend.Domain;
using MusicSitePrimeBackend.Dto;
using MusicSitePrimeBackend.Mappers;

namespace MusicSitePrimeBackend.Endpoints.Article;

public class GetAll : Endpoint<PaginationRequest, List<ArticleDto>, ArticleMapper>
{
    private readonly UnitOfWork _unitOfWork;

    public GetAll(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Get();
        Routes(ApiRoutes.ArticleEndpointsRoot);
    }

    public override async Task HandleAsync(PaginationRequest req, CancellationToken ct)
    {
        var articles = await _unitOfWork.Articles.GetAllPagedAsync(req.Skip, req.Take, ct);
        var mapped = articles.Select(article => Map.FromEntity(article)).ToList();
        await SendAsync(mapped, cancellation: ct);
    }
}