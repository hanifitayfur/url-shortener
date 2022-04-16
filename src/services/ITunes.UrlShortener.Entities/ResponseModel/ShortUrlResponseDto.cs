using System;

namespace ITunes.UrlShortener.Entities.ResponseModel
{
    public class ShortUrlResponseDto
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}