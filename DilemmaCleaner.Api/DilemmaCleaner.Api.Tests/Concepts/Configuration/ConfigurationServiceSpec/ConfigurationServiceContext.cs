using DilemmaCleaner.Api.Web.Concepts.Configuration;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;

namespace DilemmaCleaner.Api.Tests.Concepts.Configuration.ConfigurationServiceSpec;

public abstract class ConfigurationServiceContext : Specification
{
    protected ConfigurationService Service = null!;
    protected Mock<IPrismicService> PrismicMock = null!;

    protected override void EstablishContext()
    {
        PrismicMock = new Mock<IPrismicService>();

        Service = new ConfigurationService(PrismicMock.Object);
    }
}
