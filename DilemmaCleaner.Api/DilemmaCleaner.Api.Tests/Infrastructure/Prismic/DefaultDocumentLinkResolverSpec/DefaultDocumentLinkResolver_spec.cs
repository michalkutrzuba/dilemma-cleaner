using DilemmaCleaner.Api.Web.Infrastructure.Prismic;
using Microsoft.Extensions.Logging;
using prismic;
using prismic.fragments;

namespace DilemmaCleaner.Api.Tests.Infrastructure.Prismic.DefaultDocumentLinkResolverSpec;

public class when_link_has_dilemma_type : DefaultDocumentLinkResolverContext
{
    protected override void BecauseOf()
    {
        var documentLink = GetDocumentLink(CustomType.Dilemma);
        Result = Resolver.Resolve(documentLink);
    }

    [Test]
    public void it_should_return_dilemma_url()
    {
        Result.Should().Be("dilemma/testUid");
    }

    [Test]
    public void it_should_not_logged_any_error()
    {
        LoggerMock.VerifyNoOtherCalls();
    }
}

public class when_link_has_not_handled_type : DefaultDocumentLinkResolverContext
{
    protected override void BecauseOf()
    {
        var documentLink = GetDocumentLink("not-handled-type");
        Result = Resolver.Resolve(documentLink);
    }

    [Test]
    public void it_should_return_empty_url()
    {
        Result.Should().Be("/");
    }

    [Test]
    public void it_should_log_an_error()
    {
        LoggerMock.VerifyLogError("Unsuccessful resolve for type: not-handled-type with uid: testUid", Times.Once());
        LoggerMock.VerifyNoOtherCalls();
    }
}

public abstract class DefaultDocumentLinkResolverContext : Specification
{
    protected DefaultDocumentLinkResolver Resolver = null!;
    protected Mock<ILogger<DefaultDocumentLinkResolver>> LoggerMock = null!;
    protected string Result = null!;

    protected override void EstablishContext()
    {
        LoggerMock = new Mock<ILogger<DefaultDocumentLinkResolver>>();
        Resolver = new DefaultDocumentLinkResolver(LoggerMock.Object);
    }

    protected static DocumentLink GetDocumentLink(string type) => new(
        "testId",
        "testUid",
        type, new HashSet<string>(),
        "testSlug",
        "testLang",
        new Dictionary<string, IFragment>(), false
    );
}
