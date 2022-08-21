using DilemmaCleaner.Api.Web.Infrastructure.Exceptions;

namespace DilemmaCleaner.Api.Web.Infrastructure.Prismic.Exceptions;

public class MissingPrismicDocumentException : BadRequestException
{
    private readonly string _customType;
    private readonly string? _uid;

    public MissingPrismicDocumentException(string customType, string? uid = null)
    {
        _customType = customType;
        _uid = uid;
    }

    public override string Message
    {
        get
        {
            var uidMessagePart = _uid is not null ? $" with uid: {_uid} " : "";
            return $"Document of custom type: {_customType + uidMessagePart} was not found.";
        }
    }
}