using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;

namespace DilemmaCleaner.Api.Web.Concepts.Configuration;

public interface IConfigurationService
{
    Task<ConfigurationModel> Get();
}

public class ConfigurationService : IConfigurationService
{
    private readonly IPrismicService _prismicService;

    public ConfigurationService(IPrismicService prismicService)
    {
        _prismicService = prismicService;
    }
    
    public async Task<ConfigurationModel> Get()
    {
        var settingsDocument = await _prismicService.GetSettings();
        var translationsDocument = await _prismicService.GetTranslations();

        return new ConfigurationModel(settingsDocument.ToModel(), translationsDocument.ToModel());
    }
}
