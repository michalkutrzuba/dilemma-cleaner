using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.GetList;

public class when_api_returns_one_document : SingleItemContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        PrismicMock
            .Setup(prismic => prismic.GetDilemmasList())
            .ReturnsAsync(() => new DilemmasDocument(new[] { GetDocumentItem("") }));
    }

    protected override void BecauseOf()
    {
        Result = Service.GetList().Result;
    }

    [Test]
    public void it_should_return_model_with_one_document()
    {
        Result.Items.Should().HaveCount(1);
    }

    [Test]
    public void it_should_map_correctly_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[0], "");
    }
}

public abstract class SingleItemContext : GetListContext
{
}
