using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;
using DilemmaCleaner.Api.Web.Infrastructure.Cache;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;

namespace DilemmaCleaner.Api.Web.Concepts.Configuration;

public interface IConfigurationService
{
    Task<ConfigurationModel> Get();
}

public class ConfigurationService : IConfigurationService
{
    private readonly IPrismicService _prismicService;
    private readonly ICacheManager _cache;

    public ConfigurationService(IPrismicService prismicService, ICacheManager cache)
    {
        _prismicService = prismicService;
        _cache = cache;
    }

    public async Task<ConfigurationModel> Get() => await _cache.Get("config", async () =>
    {
        var settingsDocument = await _prismicService.GetSettings();
        var translationsDocument = await _prismicService.GetTranslations();

        return new ConfigurationModel(settingsDocument.ToModel(), translationsDocument.ToModel());
    });
}
