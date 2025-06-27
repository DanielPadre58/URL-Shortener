using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Repositories;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlsController(IUrlRepo repository) : ControllerBase
{
    [HttpGet("{shortenedUrlId}")]
    public ActionResult<UrlDto> GetUrl(string shortenedUrlId)
    {
        var url = repository.GetUrl(shortenedUrlId);

        return url is null ? NotFound() : url;
    }
    
    [HttpPost]
    public ActionResult<UrlDto> AddUrl(CreateUrlDto url)
    {
        var createdUrl = repository.AddUrl(url);

        return CreatedAtAction(nameof(GetUrl), new { shortenedUrlId = createdUrl.ShortenedUrlId }, createdUrl);;
    }

    [Route("urls")]
    [HttpGet("/urls/{shortenedUrlId}")]
    public ActionResult RedirectTo(string shortenedUrlId)
    {
        var url = repository.GetUrl(shortenedUrlId);

        return url is null ? NotFound() : Redirect(url.Domain);
    }
}