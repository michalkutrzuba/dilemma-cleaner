using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;
using prismic;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic;

public interface IPrismicService
{
    Task<SettingsDocument> GetSettings();
    Task<TranslationsDocument> GetTranslations();
}

public class PrismicService : IPrismicService
{
    private readonly IPrismicApiAccessor _prismic;

    public PrismicService(IPrismicApiAccessor prismic)
    {
        _prismic = prismic;
    }

    public async Task<SettingsDocument> GetSettings()
    {
        var api = await _prismic.GetApi();
        var document = await api.QueryFirst(Predicates.At("document.type", CustomType.Settings));

        var settingsDocument = new SettingsDocument(
            document.GetText($"{document.Slug}.author_email"),
            document.GetText($"{document.Slug}.author_linkedin")
        );

        return settingsDocument;
    }
}
