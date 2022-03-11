namespace MusicSitePrimeBackend.Dto;

public class ArticleDto
{
    public string Codename { get; set; }
    public string Name { get; set; }
    public string ShortText { get; set; }
    public string Text { get; set; }
    public List<string> Tags { get; set; }
    public string Status { get; set; }
    
    public Guid RelatedReleaseId { get; set; }
}