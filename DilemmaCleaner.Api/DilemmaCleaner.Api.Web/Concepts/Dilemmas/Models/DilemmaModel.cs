namespace DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

public record DilemmaModel(
    string Uid,
    string Title,
    string ImageUrl,
    string ImageAlt,
    string DescriptionHtml,
    string FirstStepId,
    IEnumerable<DilemmaModelStep> Steps
);

public record DilemmaModelStep(
    string Id,
    string Title,
    string DescriptionHtml,
    IEnumerable<DilemmaModelStepChoice> Choices
);

public record DilemmaModelStepChoice(string Text, string NextStepId);
