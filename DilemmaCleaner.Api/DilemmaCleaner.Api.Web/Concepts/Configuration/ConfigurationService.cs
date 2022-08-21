using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;

namespace DilemmaCleaner.Api.Web.Concepts.Configuration;

public interface IConfigurationService
{
    Task<ConfigurationModel> Get();
}

public class ConfigurationService : IConfigurationService
{
    public async Task<ConfigurationModel> Get()
    {
        throw new NotImplementedException();
    }
}
