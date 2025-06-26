using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Entities;

public class Url
{
    [Required]
    public string ShortenedUrlId { get; set; } = string.Empty;
    
    [Required]
    public string Domain { get; set; } = string.Empty;
}