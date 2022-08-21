using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;
using prismic;

namespace DilemmaCleaner.Api.Tests.Infrastructure.Prismic.PrismicServiceSpec.GetSettings;

[Explicit(@"prismic.API cannot be mocked. It would be great to wrap this code to test it 
properly but I will do this in my spare time. I left this test just to show how test might look.")]
public class when_api_returns_document : GetSettingsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();

        PrismicApi
            .Setup(api => api.QueryFirst(It.Is<Predicate>(predicate => predicate.Q() == "content type is Settings"), null, null))
            .ReturnsAsync(() =>
                {
                    var documentMock = new Mock<Document>();
                    documentMock.Setup(d => d.GetText("testSlug.author_email")).Returns(() => "test email");
                    documentMock.Setup(d => d.GetText("testSlug.author_linkedin")).Returns(() => "test linkedIn");

                    return documentMock.Object;
                }
            );
    }

    protected override void BecauseOf()
    {
        Result = Service.GetSettings().Result;
    }

    [Test]
    public void it_should_return_filled_document()
    {
        Result.Should().NotBeNull();
        Result.AuthorEmail.Should().Be("test email");
        Result.AuthorLinkedIn.Should().Be("test linkedIn");
    }
}

[Explicit(@"prismic.API cannot be mocked. It would be great to wrap this code to test it 
properly but I will do this in my spare time. I left this test just to show how test might look.")]
public class when_api_does_not_return_document : GetSettingsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();

        ScenarioExceptionsExpected = true;

        PrismicApi
            .Setup(api => api.QueryFirst(It.Is<Predicate>(predicate => predicate.Q() == "content type is Settings"), null, null))
            .ReturnsAsync(() => null);
    }

    protected override void BecauseOf()
    {
        Result = Service.GetSettings().Result;
    }

    [Test]
    public void it_should_not_return_anything()
    {
        Result.Should().BeNull();
    }

    [Test]
    public void it_should_throw_exception()
    {
        ScenarioException.Should().BeOfType<NullReferenceException>();
    }
}

public abstract class GetSettingsContext : PrismicServiceContext
{
    protected SettingsDocument Result = null!;
}