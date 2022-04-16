using System.Net;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Entities.ResponseModel;

namespace ITunes.UrlShortener.Entities.Maps
{
    public static class UrlShortMapper
    {
        public static ShortUrlResponseDto Map(ShortUrl shortUrl)
        {
            return new ShortUrlResponseDto
            {
                CreatedAt = shortUrl.CreatedAt,
                ExpireDate = shortUrl.ExpireDate,
                LongUrl = WebUtility.UrlDecode(shortUrl.LongUrlValue),
                ShortUrl =WebUtility.UrlDecode(shortUrl.ShortUrlValue)
            };
        }
    }
}