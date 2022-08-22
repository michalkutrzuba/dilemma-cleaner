using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

public record DilemmaDocument(
    string Uid,
    string Title,
    string ImageUrl,
    string ImageAlt,
    string DescriptionHtml,
    string FirstStepId,
    IEnumerable<DilemmaDocumentStep> Steps
)
{
    public DilemmaModel ToModel() => new(
        Uid,
        Title,
        ImageUrl,
        ImageAlt,
        DescriptionHtml,
        FirstStepId,
        Steps.Select(step => step.ToModel())
    );
}

public record DilemmaDocumentStep(
    string Id,
    string Title,
    string DescriptionHtml,
    IEnumerable<DilemmaDocumentStepChoice> Choices
)
{
    public DilemmaModelStep ToModel() => new(Id, Title, DescriptionHtml, Choices.Select(choice => choice.ToModel()));
}

public record DilemmaDocumentStepChoice(string Text, string NextStepId)
{
    public DilemmaModelStepChoice ToModel() => new(Text, NextStepId);
}
