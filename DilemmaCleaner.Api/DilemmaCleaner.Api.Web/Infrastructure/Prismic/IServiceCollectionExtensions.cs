using System.Diagnostics.CodeAnalysis;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class IServiceCollectionExtensions
{
    public static void AddPrismicDependencies(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddPrismic();
        services.Configure<PrismicSettings>(configuration.GetSection("PrismicSettings"));
    }
}
