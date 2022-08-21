using DilemmaCleaner.Api.Web.Infrastructure.Cache;

namespace DilemmaCleaner.Api.Tests;

public class FakeCacheManager : ICacheManager
{
    public async Task<TModel> Get<TModel>(string key, Func<Task<TModel>> factory)
    {
        return await factory();
    }
}