namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

public record TranslationsDocument(
    TranslationsDocumentShared Shared,
    TranslationsDocumentList List,
    TranslationsDocumentDilemma Dilemma,
    TranslationsDocumentDilemmaStep DilemmaStep,
    TranslationsDocumentDilemmaResult DilemmaResult
);

public record TranslationsDocumentShared(
    TranslationsDocumentSharedAuthor Author,
    string FooterText
);

public record TranslationsDocumentSharedAuthor(
    string SendEmail,
    string OpenLinkedIn
);

public record TranslationsDocumentList(
    string Title,
    string Description,
    string DilemmasTitle
);

public record TranslationsDocumentDilemma(
    string BackButton,
    string StartButton
);

public record TranslationsDocumentDilemmaStep(
    string FinishEarlierDividerLabel,
    string FinishEarlierButton
);

public record TranslationsDocumentDilemmaResult(
    string BackButton,
    string StartAgainButton,
    string ShowFlowchartButton,
    string HideFlowchartButton
);
