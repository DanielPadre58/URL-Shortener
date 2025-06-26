using UrlShortener.DTOs;
using UrlShortener.Entities;

namespace UrlShortener.Repositories;

public interface IUrlRepo
{
    UrlDto AddUrl(CreateUrlDto url);
    
    UrlDto? GetUrl(string shortenedUrlId);
}