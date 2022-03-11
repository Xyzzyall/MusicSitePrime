using FastEndpoints;
using MusicSitePrimeBackend.Dto;
using MusicSitePrimeBackend.Mappers;

namespace MusicSitePrimeBackend.Endpoints.Article;

public class Create : Endpoint<ArticleRequestDto, CommandResponse, ArticleMapper>
{
    public override void Configure()
    {
        Post();
        Routes(ApiRoutes.ArticleEndpointsRoot);
    }

    public override Task HandleAsync(ArticleRequestDto req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
}