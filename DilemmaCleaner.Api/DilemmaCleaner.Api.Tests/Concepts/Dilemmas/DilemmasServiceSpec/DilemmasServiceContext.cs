using DilemmaCleaner.Api.Web.Concepts.Dilemmas;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec;

public abstract class DilemmasServiceContext : Specification
{
    protected DilemmasService Service = null!;
    protected Mock<IPrismicService> PrismicMock = null!;

    protected override void EstablishContext()
    {
        PrismicMock = new Mock<IPrismicService>();

        Service = new DilemmasService(PrismicMock.Object, CacheManager);
    }
}
