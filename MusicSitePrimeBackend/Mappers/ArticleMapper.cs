using FastEndpoints;
using MusicSitePrimeBackend.Domain;
using MusicSitePrimeBackend.Domain.Models;
using MusicSitePrimeBackend.Dto;

namespace MusicSitePrimeBackend.Mappers;

public class ArticleMapper : Mapper<ArticleRequestDto, ArticleDto, Article>
{
    public override ArticleDto FromEntity(Article e)
    {
        return new ArticleDto
        {
            Codename = e.Codename,
            Name = e.Name,
            ShortText = e.ShortText,
            Text = e.Text,
            Tags = e.Tags,
            Status = e.ArticleStatus.ToString(),
            RelatedReleaseId = e.RelatedReleaseId
        };
    }
}