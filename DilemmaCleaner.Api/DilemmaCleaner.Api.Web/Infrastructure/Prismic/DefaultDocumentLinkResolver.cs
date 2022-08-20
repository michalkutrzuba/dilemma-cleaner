using prismic;
using prismic.fragments;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic;

public class DefaultDocumentLinkResolver: DocumentLinkResolver
{
    private readonly ILogger<DefaultDocumentLinkResolver> _logger;
    
    public DefaultDocumentLinkResolver(ILogger<DefaultDocumentLinkResolver> logger)
    {
        _logger = logger;
    }
    
    public override string Resolve(DocumentLink link)
    {
        if (link.Type == CustomType.Dilemma) return $"dilemma/{link.Uid}";

        var message = $"Unsuccessful resolve for type: {link.Type} with uid: {link.Uid}";
        _logger.LogError(message);

        return "/";
    }
}