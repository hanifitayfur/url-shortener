using ITunes.UrlShortener.Repositories;
using ITunes.UrlShortener.Repositories.Abstraction;
using ITunes.UrlShortener.Services;
using ITunes.UrlShortener.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITunes.UrlShortener.Api.Startups
{
    public static class DIConfiguration
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDbSettings(configuration);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IUserRepository, UserRepository>();
            services.TryAddSingleton<ICompanyRepository, CompanyRepository>();
            services.TryAddSingleton<IShortUrlRepository, ShortUrlRepository>();

            services.TryAddTransient<IAuthenticationServiceUnit, AuthenticationServiceUnit>();
            services.TryAddTransient<IShortUrlService, ShortUrlService>();
            services.TryAddTransient<ICompanyService, CompanyService>();
            services.TryAddTransient<IUserService, UserService>();
        }
    }
}