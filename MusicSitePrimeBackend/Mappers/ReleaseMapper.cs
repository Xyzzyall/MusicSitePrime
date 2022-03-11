using FastEndpoints;
using MusicSitePrimeBackend.Domain.Models;
using MusicSitePrimeBackend.Dto;

namespace MusicSitePrimeBackend.Mappers;

public class ReleaseMapper : Mapper<PaginationRequest, ReleaseDto, Release>
{
    public override ReleaseDto FromEntity(Release e)
    {
        return new ReleaseDto
        {
            Codename = e.Codename,
            Name = e.Name,
            Description = e.Description,
            ReleaseType = e.ReleaseType,
            Songs = e.Songs.Select(song => new ReleaseDto.ReleaseSongDto
            {
                Name = song.Name,
                Description = song.Description,
                Lyrics = song.Description,
                LengthSecs = song.LengthSecs
            }).ToList(),
            TotalLength = e.Songs.Sum(song => song.LengthSecs)
        };
    }
}