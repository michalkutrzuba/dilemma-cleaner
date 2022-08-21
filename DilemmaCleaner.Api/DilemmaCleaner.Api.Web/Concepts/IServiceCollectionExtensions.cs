using DilemmaCleaner.Api.Web.Concepts.Configuration;

namespace DilemmaCleaner.Api.Web.Concepts;

public static class IServiceCollectionExtensions
{
    public static void AddConcepts(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationService, ConfigurationService>();
    }
}
