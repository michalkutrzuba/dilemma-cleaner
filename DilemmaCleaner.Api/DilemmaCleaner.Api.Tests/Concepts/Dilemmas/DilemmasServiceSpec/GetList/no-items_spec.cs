using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.GetList;

public class when_api_returns_no_documents : NoItemsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        PrismicMock
            .Setup(prismic => prismic.GetDilemmasList())
            .ReturnsAsync(() => new DilemmasDocument(Array.Empty<DilemmasDocumentItem>()));
    }

    protected override void BecauseOf()
    {
        Result = Service.GetList().Result;
    }

    [Test]
    public void it_should_return_model_without_items()
    {
        Result.Items.Should().BeEmpty();
    }
}

public class when_there_is_problem_with_fetching_dilemmas : NoItemsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        ScenarioExceptionsExpected = true;

        PrismicMock
            .Setup(prismic => prismic.GetDilemmasList())
            .ThrowsAsync(new Exception("GetDilemmasList exception"));
    }

    protected override void BecauseOf()
    {
        Result = Service.GetList().Result;
    }

    [Test]
    public void it_should_not_return_model()
    {
        Result.Should().BeNull();
    }

    [Test]
    public void it_should_throw_translations_error()
    {
        ScenarioException.Should().BeOfType<AggregateException>();
        ScenarioException.InnerException.Should().BeOfType<Exception>();
        ScenarioException.InnerException!.Message.Should().Be("GetDilemmasList exception");
    }
}

public abstract class NoItemsContext : GetListContext
{
}
