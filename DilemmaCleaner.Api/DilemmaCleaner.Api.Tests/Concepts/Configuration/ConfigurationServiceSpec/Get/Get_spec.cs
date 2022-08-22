using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

namespace DilemmaCleaner.Api.Tests.Concepts.Configuration.ConfigurationServiceSpec.Get;

public class when_all_data_are_fetched : GetContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        SetupGetSettingsValidResponse();
        SetupGetTranslationsValidResponse();
    }

    protected override void BecauseOf()
    {
        Result = Service.Get().Result;
    }

    [Test]
    public void it_should_return_valid_model()
    {
        VerifyGetSettingsValidResponse(Result.Settings);
        VerifyGetTranslationsValidResponse(Result.Translations);
    }
}

public class when_there_is_problem_with_settings_document : GetContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        ScenarioExceptionsExpected = true;

        SetupGetSettingsErrorResponse();
        SetupGetTranslationsValidResponse();
    }

    protected override void BecauseOf()
    {
        Result = Service.Get().Result;
    }

    [Test]
    public void it_should_not_return_model()
    {
        Result.Should().BeNull();
    }

    [Test]
    public void it_should_throw_settings_error()
    {
        ScenarioException.Should().BeOfType<AggregateException>();
        ScenarioException.InnerException.Should().BeOfType<Exception>();
        ScenarioException.InnerException!.Message.Should().Be("GetSettings exception");
    }
}

public class when_there_is_problem_with_translation_document : GetContext
{
    protected override void EstablishContext()
    {
        base.EstablishContext();
        ScenarioExceptionsExpected = true;

        SetupGetSettingsValidResponse();
        SetupGetTranslationsErrorResponse();
    }

    protected override void BecauseOf()
    {
        Result = Service.Get().Result;
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
        ScenarioException.InnerException!.Message.Should().Be("GetTranslations exception");
    }
}

public abstract class GetContext : ConfigurationServiceContext
{
    protected ConfigurationModel Result = null!;

    #region Prismic.GetSettings

    protected void SetupGetSettingsValidResponse()
    {
        PrismicMock
            .Setup(prismic => prismic.GetSettings())
            .ReturnsAsync(() => new SettingsDocument("testEmail", "testLinkedIn"));
    }

    protected void SetupGetSettingsErrorResponse()
    {
        PrismicMock
            .Setup(prismic => prismic.GetSettings())
            .ThrowsAsync(new Exception("GetSettings exception"));
    }

    protected void VerifyGetSettingsValidResponse(SettingsModel settingsModel)
    {
        settingsModel.AuthorEmail.Should().Be("testEmail");
        settingsModel.AuthorLinkedIn.Should().Be("testLinkedIn");
    }

    #endregion

    #region Prismic.GetTranslations

    protected void SetupGetTranslationsValidResponse()
    {
        var translationDocument = new TranslationsDocument(
            new TranslationsDocumentShared(
                new TranslationsDocumentSharedAuthor("testSendEmail", "testOpenLinkedIn"),
                "testFooterText"
            ),
            new TranslationsDocumentList("testTitle", "testDescription", "testDilemmasTitle"),
            new TranslationsDocumentDilemma("testBackButton", "testStartButton"),
            new TranslationsDocumentDilemmaStep("testFinishEarlierDividerLabel", "testFinishEarlierButton"),
            new TranslationsDocumentDilemmaResult(
                "testBackButton",
                "testStartAgainButton",
                "testShowFlowchartButton",
                "testHideFlowchartButton"
            )
        );

        PrismicMock
            .Setup(prismic => prismic.GetTranslations())
            .ReturnsAsync(() => translationDocument);
    }

    protected void SetupGetTranslationsErrorResponse()
    {
        PrismicMock
            .Setup(prismic => prismic.GetTranslations())
            .ThrowsAsync(new Exception("GetTranslations exception"));
    }

    protected void VerifyGetTranslationsValidResponse(TranslationsModel translationsModel)
    {
        var shared = translationsModel.Shared;
        shared.Author.SendEmail.Should().Be("testSendEmail");
        shared.Author.OpenLinkedIn.Should().Be("testOpenLinkedIn");
        shared.FooterText.Should().Be("testFooterText");

        var list = translationsModel.List;
        list.Title.Should().Be("testTitle");
        list.Description.Should().Be("testDescription");
        list.DilemmasTitle.Should().Be("testDilemmasTitle");

        var dilemmaPage = translationsModel.Dilemma.Page;
        dilemmaPage.BackButton.Should().Be("testBackButton");
        dilemmaPage.StartButton.Should().Be("testStartButton");

        var dilemmaStep = translationsModel.Dilemma.Step;
        dilemmaStep.FinishEarlierDividerLabel.Should().Be("testFinishEarlierDividerLabel");
        dilemmaStep.FinishEarlierButton.Should().Be("testFinishEarlierButton");

        var dilemmaResult = translationsModel.Dilemma.Result;
        dilemmaResult.BackButton.Should().Be("testBackButton");
        dilemmaResult.StartAgainButton.Should().Be("testStartAgainButton");
        dilemmaResult.ShowFlowchartButton.Should().Be("testShowFlowchartButton");
        dilemmaResult.HideFlowchartButton.Should().Be("testHideFlowchartButton");
    }

    #endregion
}
