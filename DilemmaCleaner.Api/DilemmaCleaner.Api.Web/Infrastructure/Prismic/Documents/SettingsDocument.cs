using DilemmaCleaner.Api.Web.Concepts.Configuration.Models;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

public record SettingsDocument(
    string AuthorEmail,
    string AuthorLinkedIn
)
{
    public SettingsModel ToModel() => new(AuthorEmail, AuthorLinkedIn);
}