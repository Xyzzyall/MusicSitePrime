using Microsoft.AspNetCore.Mvc;

namespace MusicSitePrimeBackend.Dto;

public class PaginationRequest
{
    [FromQuery] public int Skip { get; set; }
    [FromQuery] public int Take { get; set; }
}