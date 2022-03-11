using FastEndpoints;
using MusicSitePrimeBackend.Dto;
using MusicSitePrimeBackend.Mappers;

namespace MusicSitePrimeBackend.Endpoints.Article;

public class Update : Endpoint<UpdateCommand<ArticleRequestDto>, CommandResponse, ArticleMapper>
{
    public override void Configure()
    {
        Put();
        Routes(ApiRoutes.ArticleEndpointsRoot + "/{id}");
    }

    public override Task HandleAsync(UpdateCommand<ArticleRequestDto> req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
}