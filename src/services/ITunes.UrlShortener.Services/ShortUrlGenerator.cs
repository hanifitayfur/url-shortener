using System;
using System.Linq;

namespace ITunes.UrlShortener.Services
{
    public static class ShortUrlGenerator
    {
        public static string Generate()
        {
            var url = string.Empty;
            Enumerable.Range(48, 75)
                .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
                .OrderBy(o => new Random().Next())
                .ToList()
                .ForEach(i => url += Convert.ToChar(i));
            var shortUrl = url.Substring(new Random().Next(0, url.Length), new Random().Next(2, 7));

            return shortUrl.ToLower();
        }
    }
}