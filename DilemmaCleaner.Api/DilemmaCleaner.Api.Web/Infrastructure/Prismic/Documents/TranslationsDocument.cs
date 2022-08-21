using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

public record TranslationsDocument(
    TranslationsDocumentShared Shared,
    TranslationsDocumentList List,
    TranslationsDocumentDilemma Dilemma,
    TranslationsDocumentDilemmaStep DilemmaStep,
    TranslationsDocumentDilemmaResult DilemmaResult
)
{
    public TranslationsModel ToModel() => new(
        Shared.ToModel(),
        List.ToModel(),
        new TranslationsModelDilemma(Dilemma.ToModel(), DilemmaStep.ToModel(), DilemmaResult.ToModel())
    );
}

public record TranslationsDocumentShared(
    TranslationsDocumentSharedAuthor Author,
    string FooterText
)
{
    public TranslationsModelShared ToModel() => new(Author.ToModel(), FooterText);
}

public record TranslationsDocumentSharedAuthor(
    string SendEmail,
    string OpenLinkedIn
)
{
    public TranslationsModelSharedAuthor ToModel() => new(SendEmail, OpenLinkedIn);
}

public record TranslationsDocumentList(
    string Title,
    string Description,
    string DilemmasTitle
)
{
    public TranslationsModelList ToModel() => new(Title, Description, DilemmasTitle);
}

public record TranslationsDocumentDilemma(
    string BackButton,
    string StartButton
)
{
    public TranslationsModelDilemmaPage ToModel() => new(BackButton, StartButton);
}

public record TranslationsDocumentDilemmaStep(
    string FinishEarlierDividerLabel,
    string FinishEarlierButton
)
{
    public TranslationsModelDilemmaStep ToModel() => new(FinishEarlierDividerLabel, FinishEarlierButton);
}

public record TranslationsDocumentDilemmaResult(
    string BackButton,
    string StartAgainButton,
    string ShowFlowchartButton,
    string HideFlowchartButton
)
{
    public TranslationsModelDilemmaResult ToModel() => new(
        BackButton,
        StartAgainButton,
        ShowFlowchartButton,
        HideFlowchartButton
    );
}
