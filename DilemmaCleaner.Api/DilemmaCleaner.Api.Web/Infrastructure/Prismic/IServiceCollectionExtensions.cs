using prismic;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic;

public static class IServiceCollectionExtensions
{
    public static void AddPrismicDependencies(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddPrismic();
        services.Configure<PrismicSettings>(configuration.GetSection("PrismicSettings"));
    }
}
