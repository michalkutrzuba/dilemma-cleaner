using DilemmaCleaner.Api.Web.Infrastructure.Prismic;
using prismic;

namespace DilemmaCleaner.Api.Tests.Infrastructure.Prismic.PrismicServiceSpec;

public abstract class PrismicServiceContext : Specification
{
    protected PrismicService Service = null!;
    protected Mock<prismic.Api> PrismicApi = null!;
    protected Mock<IPrismicApiAccessor> PrismicMock = null!;

    protected override void EstablishContext()
    {
        PrismicApi = new Mock<prismic.Api>();
        PrismicMock = new Mock<IPrismicApiAccessor>();
        PrismicMock.Setup(prismic => prismic.GetApi()).ReturnsAsync(() => PrismicApi.Object);

        Service = new PrismicService(PrismicMock.Object);
    }
}
