using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

public record DilemmasDocument(IEnumerable<DilemmasDocumentItem> Items)
{
    public DilemmasListModel ToModel() => new(Items.Select(item => item.ToModel()));
}

public record DilemmasDocumentItem(string Uid, string Title, string ImageUrl, string ImageAlt)
{
    public DilemmasListModelItem ToModel() => new(Uid, Title, ImageUrl, ImageAlt);
}
