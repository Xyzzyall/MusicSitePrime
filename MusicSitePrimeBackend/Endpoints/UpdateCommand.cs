using Microsoft.AspNetCore.Mvc;

namespace MusicSitePrimeBackend.Endpoints;

public class UpdateCommand<TDto>
where TDto : class
{
    [FromQuery] public Guid Id { get; set; }
    [FromBody] public TDto Data { get; set; }
}