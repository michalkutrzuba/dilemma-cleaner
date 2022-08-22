namespace DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;

public record DilemmasListModel(IEnumerable<DilemmasListModelItem> Items);

public record DilemmasListModelItem(string Uid, string Title, string ImageUrl, string ImageAlt);
