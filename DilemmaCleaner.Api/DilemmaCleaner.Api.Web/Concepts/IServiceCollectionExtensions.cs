using DilemmaCleaner.Api.Web.Concepts.Configuration;
using DilemmaCleaner.Api.Web.Concepts.Dilemmas;

namespace DilemmaCleaner.Api.Web.Concepts;

public static class IServiceCollectionExtensions
{
    public static void AddConcepts(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<IDilemmasService, DilemmasService>();
    }
}
