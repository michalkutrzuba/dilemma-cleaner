using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Exceptions;
using prismic;
using prismic.fragments;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic;

public interface IPrismicService
{
    Task<SettingsDocument> GetSettings();
    Task<TranslationsDocument> GetTranslations();
    Task<DilemmasDocument> GetDilemmasList();
    Task<DilemmaDocument> GetDilemma(string uid);
}

public class PrismicService : IPrismicService
{
    private readonly IPrismicApiAccessor _prismic;
    private readonly DocumentLinkResolver _documentLinkResolver;

    public PrismicService(IPrismicApiAccessor prismic, DocumentLinkResolver documentLinkResolver)
    {
        _prismic = prismic;
        _documentLinkResolver = documentLinkResolver;
    }

    public async Task<SettingsDocument> GetSettings()
    {
        var api = await _prismic.GetApi();
        var document = await api.QueryFirst(Predicates.At("document.type", CustomType.Settings));

        if (document is null)
            throw new MissingPrismicDocumentException(CustomType.Settings);

        var settingsDocument = new SettingsDocument(
            document.GetText($"{document.Slug}.author_email"),
            document.GetText($"{document.Slug}.author_linkedin")
        );

        return settingsDocument;
    }

    public async Task<TranslationsDocument> GetTranslations()
    {
        var api = await _prismic.GetApi();
        var document = await api.QueryFirst(Predicates.At("document.type", CustomType.Translations));

        if (document is null)
            throw new MissingPrismicDocumentException(CustomType.Translations);

        var shared = new TranslationsDocumentShared(
            new TranslationsDocumentSharedAuthor(
                document.GetText($"{document.Slug}.author_send_email"),
                document.GetText($"{document.Slug}.author_open_linked_in")
            ),
            document.GetText($"{document.Slug}.footer_text")
        );

        var list = new TranslationsDocumentList(
            document.GetText($"{document.Slug}.list_title"),
            document.GetStructuredText($"{document.Slug}.list_description").AsHtml(_documentLinkResolver),
            document.GetText($"{document.Slug}.list_dilemmas_title")
        );

        var dilemma = new TranslationsDocumentDilemma(
            document.GetText($"{document.Slug}.dilemma_back_button"),
            document.GetText($"{document.Slug}.dilemma_start_button")
        );
        
        var dilemmaStep = new TranslationsDocumentDilemmaStep(
            document.GetText($"{document.Slug}.dilemma_step_finish_earlier_divider_label"),
            document.GetText($"{document.Slug}.dilemma_step_finish_earlier_button")
        );
        
        var dilemmaResult = new TranslationsDocumentDilemmaResult(
            document.GetText($"{document.Slug}.dilemma_result_back_button"),
            document.GetText($"{document.Slug}.dilemma_result_start_again_button"),
            document.GetText($"{document.Slug}.dilemma_result_show_flowchart_button"),
            document.GetText($"{document.Slug}.dilemma_result_hide_flowchart_button")
        );

        var translationsDocument = new TranslationsDocument(
            shared,
            list,
            dilemma,
            dilemmaStep,
            dilemmaResult
        );

        return translationsDocument;
    }

    public async Task<DilemmasDocument> GetDilemmasList()
    {
        var api = await _prismic.GetApi();
        var documents = await api
            .Query(Predicates.At("document.type", CustomType.Dilemma))
            .Submit();

        var items = documents.Results.Select(document =>
        {
            var image = document.GetImageView($"{CustomType.Dilemma}.image", "main");

            var dilemmaDocument = new DilemmasDocumentItem(
                document.Uid,
                document.GetText($"{CustomType.Dilemma}.title"),
                image.Url,
                image.Alt
            );

            return dilemmaDocument;
        });

        return new DilemmasDocument(items);
    }

    public async Task<DilemmaDocument> GetDilemma(string uid)
    {
        var api = await _prismic.GetApi();
        var document = await api.GetByUID(CustomType.Dilemma, uid);

        var stepDocuments = new List<DilemmaDocumentStep>();
        var image = document.GetImageView($"{CustomType.Dilemma}.image", "main");
        var firstStep = (DocumentLink)document.Get($"{CustomType.Dilemma}.first_step");

        await ResolveStep(firstStep.Id);

        var dilemmaDocument = new DilemmaDocument(
            document.Uid,
            document.GetText($"{CustomType.Dilemma}.title"),
            image.Url,
            image.Alt,
            document.GetStructuredText($"{CustomType.Dilemma}.description").AsHtml(_documentLinkResolver),
            firstStep.Id,
            stepDocuments
        );

        return dilemmaDocument;

        // local functions

        async Task ResolveStep(string id)
        {
            if (stepDocuments.Any(stepDocument => stepDocument.Id == id)) return;

            var step = await api.GetByID(id);
            var choices = step.GetGroup($"{CustomType.DilemmaStep}.choices").GroupDocs ?? new List<GroupDoc>();
            var choiceDocuments = choices
                .Where(choice => choice.Fragments.Any())
                .Select(choice =>
                {
                    var nextStep = (DocumentLink)choice.Get("next_step");
                    return new DilemmaDocumentStepChoice(choice.GetText("text"), nextStep.Id);
                })
                .ToList();

            var stepDocument = new DilemmaDocumentStep(
                step.Id,
                step.GetText($"{CustomType.DilemmaStep}.title"),
                step.GetStructuredText($"{CustomType.DilemmaStep}.description")?.AsHtml(_documentLinkResolver) ?? string.Empty,
                choiceDocuments
            );

            stepDocuments.Add(stepDocument);

            foreach (var choiceDocument in choiceDocuments)
            {
                await ResolveStep(choiceDocument.NextStepId);
            }
        }
    }
}
