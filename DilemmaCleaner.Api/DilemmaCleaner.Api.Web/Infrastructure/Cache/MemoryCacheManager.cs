using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DilemmaCleaner.Api.Web.Infrastructure.Cache;

public interface ICacheManager
{
    Task<TModel> Get<TModel>(string key, Func<Task<TModel>> factory);
}

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _settings;

    public MemoryCacheManager(IMemoryCache cache, IOptions<CacheSettings> settings)
    {
        _cache = cache;
        _settings = settings.Value;
    }

    public async Task<TModel> Get<TModel>(string key, Func<Task<TModel>> factory)
    {
        if (_settings.Enabled is false)
            return await factory();

        return await _cache.GetOrCreateAsync(key, cacheEntry =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(_settings.SlidingExpirationInSeconds ?? 0);
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_settings.AbsoluteExpirationInSeconds ?? 0);

            return factory();
        });
    }
}