using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.GetList;

public class when_method_is_called : GetListContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        ScenarioExceptionsExpected = true;
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
    public void it_should_throw_not_implemented_exception()
    {
        ScenarioException.Should().BeOfType<AggregateException>();
        ScenarioException.InnerException.Should().BeOfType<NotImplementedException>();
    }
}

public abstract class GetListContext : DilemmasServiceContext
{
    protected DilemmasListModel Result = null!;
}
