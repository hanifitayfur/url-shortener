using ITunes.UrlShortener.Entities.AppSettings;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Repositories.Abstraction;
using Microsoft.Extensions.Options;

namespace ITunes.UrlShortener.Repositories
{
    public class ShortUrlRepository : MongoDbBaseRepository<ShortUrl>, IShortUrlRepository
    {
        public ShortUrlRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}