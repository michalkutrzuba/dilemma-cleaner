namespace DilemmaCleaner.Api.Web.Infrastructure.Cache;

public class CacheSettings
{
    public bool Enabled { get; set; }
    public double? SlidingExpirationInSeconds { get; set; }
    public double? AbsoluteExpirationInSeconds { get; set; }
}