namespace MusicSitePrimeBackend.Dto;

public class ReleaseDto
{
    //public string Id { get; set; }

    public string Codename { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ReleaseType { get; set; }
    public List<ReleaseSongDto> Songs { get; set; }
    public int TotalLength { get; set; }
    
    public class ReleaseSongDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Lyrics { get; set; }
        public int LengthSecs { get; set; }
    }
}