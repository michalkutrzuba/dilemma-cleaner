namespace DilemmaCleaner.Api.Web.Infrastructure.Cache;

public static class IServiceCollectionExtensions
{
    public static void AddCacheDependencies(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMemoryCache();
        services.Configure<CacheSettings>(configuration.GetSection("Cache"));
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
    }
}
