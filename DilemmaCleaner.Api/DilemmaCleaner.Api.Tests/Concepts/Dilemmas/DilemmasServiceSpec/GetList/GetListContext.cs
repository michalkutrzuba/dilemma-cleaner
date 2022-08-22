using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic.Documents;

namespace DilemmaCleaner.Api.Tests.Concepts.Dilemmas.DilemmasServiceSpec.GetList;

public abstract class GetListContext : DilemmasServiceContext
{
    protected DilemmasListModel Result = null!;

    protected DilemmasDocumentItem GetDocumentItem(string suffix)
    {
        return new DilemmasDocumentItem(
            "testUid" + suffix,
            "testTitle" + suffix,
            "testImageUrl" + suffix,
            "testImageAlt" + suffix
        );
    }

    protected void VerifyModelItem(DilemmasListModelItem item, string suffix)
    {
        item.Uid.Should().Be("testUid" + suffix);
        item.Title.Should().Be("testTitle" + suffix);
        item.ImageUrl.Should().Be("testImageUrl" + suffix);
        item.ImageAlt.Should().Be("testImageAlt" + suffix);
    }
}
