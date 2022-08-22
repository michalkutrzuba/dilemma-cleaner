using DilemmaCleaner.Api.Web.Concepts.Dilemmas.Models;
using DilemmaCleaner.Api.Web.Infrastructure.Cache;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;

namespace DilemmaCleaner.Api.Web.Concepts.Dilemmas;

public interface IDilemmasService
{
    Task<DilemmasListModel> GetList();
    Task<DilemmaModel> Get(string uid);
}

public class DilemmasService : IDilemmasService
{
    private readonly IPrismicService _prismicService;
    private readonly ICacheManager _cache;

    public DilemmasService(IPrismicService prismicService, ICacheManager cache)
    {
        _prismicService = prismicService;
        _cache = cache;
    }

    public async Task<DilemmasListModel> GetList() => await _cache.Get("dilemmas", async () =>
    {
        var dilemmasDocument = await _prismicService.GetDilemmasList();

        return dilemmasDocument.ToModel();
    });

    public async Task<DilemmaModel> Get(string uid) => await _cache.Get<DilemmaModel>($"dilemma_{uid}", async () =>
    {
        throw new NotImplementedException();
    });
}
