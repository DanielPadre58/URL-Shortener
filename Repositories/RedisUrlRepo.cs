using System.Text.Json;
using StackExchange.Redis;
using UrlShortener.DTOs;

namespace UrlShortener.Repositories;

public class RedisUrlRepo : IUrlRepo
{
    private readonly IConnectionMultiplexer _redis;

    private readonly IDatabase _db;
    
    const string shortenUrlPath = "/urls/";

    public RedisUrlRepo(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = _redis.GetDatabase();
    }

    public UrlDto AddUrl(CreateUrlDto url)
    {
        var urlId = Guid.NewGuid().ToString()[..8];
        
        var newUrl = new  UrlDto(url.Domain, urlId);
        
        var serialUrl = JsonSerializer.Serialize(newUrl);
        
        _db.StringSet(newUrl.ShortenedUrlId, serialUrl);
        
        return newUrl;
    }

    public UrlDto? GetUrl(string shortenedUrlId)
    {
        if(string.IsNullOrEmpty(shortenedUrlId)) throw new ArgumentNullException();
        
        var serialUrl = _db.StringGet(shortenedUrlId);
        
        return serialUrl.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UrlDto>(serialUrl);
    }
}