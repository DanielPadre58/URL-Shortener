namespace UrlShortener.DTOs;

public record UrlDto(
    string Domain,
    string ShortenedUrlId
);