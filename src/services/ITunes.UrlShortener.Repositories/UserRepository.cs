using ITunes.UrlShortener.Entities.AppSettings;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Repositories.Abstraction;
using Microsoft.Extensions.Options;

namespace ITunes.UrlShortener.Repositories
{
    public class UserRepository : MongoDbBaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}