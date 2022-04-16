using ITunes.UrlShortener.Entities.Entities.MongoDB;

namespace ITunes.UrlShortener.Repositories.Abstraction
{
    public interface IUserRepository : IRepository<User, string>
    {
    }
}