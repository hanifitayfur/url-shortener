
using ltunes.Core.Cache.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace lTunes.Core.Cache.Memory
{
    public static class MemoryCacheInjectionExtension
    {
        public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
        {
            return services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}