namespace DilemmaCleaner.Api.Web.Concepts.Configuration.Models;

public record TranslationsModel(
    TranslationsModelShared Shared,
    TranslationsModelList List,
    TranslationsModelDilemma Dilemma
);

public record TranslationsModelShared(
    TranslationsModelSharedAuthor Author,
    string FooterText
);

public record TranslationsModelSharedAuthor(
    string SendEmail,
    string OpenLinkedIn
);

public record TranslationsModelList(
    string Title,
    string Description,
    string DilemmasTitle
);

public record TranslationsModelDilemma(
    TranslationsModelDilemmaPage Page,
    TranslationsModelDilemmaStep Step,
    TranslationsModelDilemmaResult Result
);

public record TranslationsModelDilemmaPage(
    string BackButton,
    string StartButton
);

public record TranslationsModelDilemmaStep(
    string FinishEarlierDividerLabel,
    string FinishEarlierButton
);

public record TranslationsModelDilemmaResult(
    string BackButton,
    string StartAgainButton,
    string ShowFlowchartButton,
    string HideFlowchartButton
);
