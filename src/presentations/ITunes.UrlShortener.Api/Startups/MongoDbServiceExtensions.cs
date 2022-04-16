using ITunes.UrlShortener.Entities.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITunes.UrlShortener.Api.Startups
{
    public static class MongoDbServiceExtensions
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });
        }
    }
}