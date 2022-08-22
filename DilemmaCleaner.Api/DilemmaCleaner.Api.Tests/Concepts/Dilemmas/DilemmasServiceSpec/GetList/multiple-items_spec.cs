using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.GetList;

public class when_api_returns_two_documents : MultipleItemsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        PrismicMock
            .Setup(prismic => prismic.GetDilemmasList())
            .ReturnsAsync(() => new DilemmasDocument(new[]
            {
                GetDocumentItem("First"),
                GetDocumentItem("Second")
            }));
    }

    protected override void BecauseOf()
    {
        Result = Service.GetList().Result;
    }

    [Test]
    public void it_should_return_model_with_two_documents()
    {
        Result.Items.Should().HaveCount(2);
    }

    [Test]
    public void it_should_map_correctly_first_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[0], "First");
    }
    
    [Test]
    public void it_should_map_correctly_second_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[1], "Second");
    }
}

public class when_api_returns_three_documents : MultipleItemsContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        PrismicMock
            .Setup(prismic => prismic.GetDilemmasList())
            .ReturnsAsync(() => new DilemmasDocument(new[]
            {
                GetDocumentItem("First"),
                GetDocumentItem("Second"),
                GetDocumentItem("Third")
            }));
    }

    protected override void BecauseOf()
    {
        Result = Service.GetList().Result;
    }

    [Test]
    public void it_should_return_model_with_three_documents()
    {
        Result.Items.Should().HaveCount(3);
    }

    [Test]
    public void it_should_map_correctly_first_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[0], "First");
    }
    
    [Test]
    public void it_should_map_correctly_second_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[1], "Second");
    }

    [Test]
    public void it_should_map_correctly_third_item_model()
    {
        var items = Result.Items.ToList();
        VerifyModelItem(items[2], "Third");
    }
}

public abstract class MultipleItemsContext : GetListContext
{
}
